using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MgebroStore.Data;
using MgebroStore.Data.Entities;
using MgebroStore.Models.Sale;
using MgebroStore.Models.Error;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MgebroStore.Controllers
{
    public class SalesController : Controller
    {
        private readonly StoreContext _context;

        public SalesController(StoreContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            return View(await _context.Sales.ToListAsync());
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var sale = await _context.Sales.FirstOrDefaultAsync(m => m.ID == id);
            if (sale == null) return NotFound();

            var seller = JObject.Parse(sale.SellerInfo);

            SaleDetailsViewModel sd = new SaleDetailsViewModel();

            sd.SaleDate = sale.SaleDate;
            sd.TotalPrice = sale.TotalPrice;

            sd.PID = seller["PID"].ToString();
            sd.Seller = seller["FullName"].ToString();

            sd.SoldItems = JsonConvert.DeserializeObject<List<SaleItem>>(sale.Description);

            return View(sd);
        }



        public IActionResult Create()
        {
            List<SelectListItem> selDict = new List<SelectListItem>();
            selDict.AddRange(_context.Consultants.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.LastName + " " + c.FirstName }).ToList());
            ViewBag.Sellers = selDict;

            List<SelectListItem> proDict = new List<SelectListItem>();
            proDict.AddRange(_context.Products.Select(p => new SelectListItem { Value = p.ID.ToString(), Text = p.Title }).ToList());
            ViewBag.Products = proDict;

            //CreateSaleViewModel vm = new CreateSaleViewModel();

            return View();
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SellerID,ProductIDsText,ProductQuantityText")] CreateSaleViewModel sv)
        {
            
            if (!sv.CastVariables())
            {
                return View("GeneralError", new GeneralErrorViewModel { Text = "დაფიქსირდა შეცდომა მონაცემების დამუშავებისას!" });
            }

            if (sv.ProductIDs is null || sv.ProductIDs.Count == 0)
            {
                return View("GeneralError", new GeneralErrorViewModel { Text = "თქვენ არ გაქვთ არჩეული პროდუქტი!" });
            }

            Sale sale = new Sale();

            var consultant = _context.Consultants.FirstOrDefault(c => c.ID == sv.SellerID);

            if(consultant == null)
            {
                return View("GeneralError", new GeneralErrorViewModel { Text = "კონსულტანტი ვერ მოიძებნა!" });
            }



            List<SaleItem> saleItems = new List<SaleItem>();



            var products = _context.Products.Where(p => sv.ProductIDs.Contains(p.ID)).ToList();
            float totalPrice = 0;

            for(int i = 0; i < sv.ProductIDs.Count; i++)
            {
                var prod = products.FirstOrDefault(p => p.ID == sv.ProductIDs[i]);

                saleItems.Add(new SaleItem()
                {
                    Code = prod.Code,
                    Price = prod.Price,
                    Title = prod.Title,
                    Quantity = sv.ProductQuantity[i]
                });

                totalPrice += prod.Price * sv.ProductQuantity[i];
            }

            sale.TotalPrice = totalPrice;

            var sellerInfo = new
            {
                PID = consultant.PID,
                FullName = consultant.GetFullName()
            };

            sale.SellerInfo = JsonConvert.SerializeObject(sellerInfo);
            sale.Description = JsonConvert.SerializeObject(saleItems);
            sale.SaleDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sale);
        }



        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var sale = await _context.Sales.FindAsync(id);
        //    if (sale == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(sale);
        //}



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,SaleDate,SellerInfo,Description")] Sale sale)
        //{
        //    if (id != sale.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(sale);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SaleExists(sale.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(sale);
        //}




        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.ID == id);
        }
    }
}
