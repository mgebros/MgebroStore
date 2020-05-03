using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MgebroStore.Data;
using MgebroStore.Data.Entities;
using MgebroStore.Models.Error;
using MgebroStore.Models.Consultant;
using Newtonsoft.Json.Linq;

namespace MgebroStore.Controllers
{
    public class ConsultantsController : Controller
    {
        private readonly StoreContext _context;

        public ConsultantsController(StoreContext context)
        {
            _context = context;
        }



        private void FillReferralNames(List<Consultant> cons)
        {
            foreach (var con in cons)
            {
                if (con.ReferralID != 0)
                    con.ReferralName = cons.FirstOrDefault(c => c.ID == con.ReferralID).GetFullName();
                else
                    con.ReferralName = "ცარიელი";
            }
        }

        private void FillReferralName(Consultant con)
        {
            if (con.ReferralID != 0)
                con.ReferralName = _context.Consultants.FirstOrDefault(c => c.ID == con.ReferralID).GetFullName();
            else
                con.ReferralName = "ცარიელი";
        }



        public async Task<IActionResult> Index()
        {

            var cons = await _context.Consultants.ToListAsync();
            FillReferralNames(cons);



            return View(cons);
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultants
                .FirstOrDefaultAsync(m => m.ID == id);

            FillReferralName(consultant);

            if (consultant == null)
            {
                return NotFound();
            }

            return View(consultant);
        }



        public IActionResult Create()
        {
            List<SelectListItem> refDict = new List<SelectListItem>();
            refDict.Add(new SelectListItem("ცარიელი", "0"));
            refDict.AddRange(_context.Consultants.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.LastName + " " + c.FirstName }).ToList());
            ViewBag.Referrals = refDict;

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,PID,Gender,Birthdate,ReferralID")] Consultant consultant)
        {
            if (consultant.ReferralID != 0) {
                var exists = _context.Consultants.FirstOrDefault(c => c.ID == consultant.ReferralID);

                if (exists is null)
                    consultant.ReferralID = 0;
            }

            if (ModelState.IsValid)
            {
                _context.Add(consultant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consultant);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)  return NotFound();

            var consultant = await _context.Consultants.FindAsync(id);
            if (consultant == null) return NotFound();

            List<SelectListItem> refDict = new List<SelectListItem>();
            refDict.Add(new SelectListItem("ცარიელი", "0"));
            refDict.AddRange(_context.Consultants.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.LastName + " " + c.FirstName }).ToList());
            ViewBag.Referrals = refDict;

            return View(consultant);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,PID,Gender,Birthdate,ReferralID")] Consultant consultant)
        {
            //  Restrict being of self referral
            if (consultant.ID == consultant.ReferralID)
            {
                return View("GeneralError", new GeneralErrorViewModel { Text = "კონსულტანტი საკუთარი თავის რეკომენდატორი არ უნდა იყოს!" });
            }

            //  Restrict being referral of your referrals
            var gettingReferralOfYourReferal = _context.Consultants.Where(c => c.ReferralID == consultant.ID).Any(c => c.ID == consultant.ReferralID);

            if (gettingReferralOfYourReferal)
            {
                return View("GeneralError", new GeneralErrorViewModel { Text = "არ შეიძლება კონსულტანტის რეკომენდატორი მისივე მოწვეული კონსულტანტი იყოს!" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultantExists(consultant.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(consultant);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultants
                .FirstOrDefaultAsync(m => m.ID == id);

            FillReferralName(consultant);

            if (consultant == null)
            {
                return NotFound();
            }

            return View(consultant);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultant = await _context.Consultants.FindAsync(id);
            _context.Consultants.Remove(consultant);

            _context.Consultants.Where(c => c.ReferralID == id).ToList().ForEach(c =>
              {
                  c.ReferralID = 0;
              });

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultantExists(int id)
        {
            return _context.Consultants.Any(e => e.ID == id);
        }



        //კონსულტანტები ხშირად გაყიდვადი პროდუქტების მიხედვით:
        [HttpGet, ActionName("SearchByFrequentlySoldProducts")]
        public async Task<IActionResult> SearchByFrequentlySoldProducts()
        {
            SearchByFrequentlySoldProductsViewModel vm = new SearchByFrequentlySoldProductsViewModel();
            return View(vm);
        }



        class ProdQuantityPair
        {
            public int Code { get; set; }
            public int Quantity { get; set; }
        }

        class PIDQuantityPair
        {
            public string PID { get; set; }
            public int Quantity { get; set; }
        }

        //კონსულტანტები ხშირად გაყიდვადი პროდუქტების მიხედვით:
        [HttpPost, ActionName("SearchByFrequentlySoldProducts")]
        public async Task<IActionResult> SearchByFrequentlySoldProducts(SearchByFrequentlySoldProductsRequest req)
        {
            //  Include last day's sales
            req.ToDate = req.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            SearchByFrequentlySoldProductsViewModel vm = new SearchByFrequentlySoldProductsViewModel();



            var pqp = new List<PIDQuantityPair>();

            if (req.Code != 0)
            {
                //  Get sales in Date range

                var sales = _context.Sales.Where(s => s.SaleDate >= req.FromDate && s.SaleDate <= req.ToDate).ToList();



                //  Get All PID-Quantity pairs among sales

                foreach (var sale in sales)
                {
                    var pid = JObject.Parse(sale.SellerInfo)["PID"].ToString();


                    var exists = pqp.FirstOrDefault(pp => pp.PID == pid);

                    if (exists == null) { 
                        pqp.Add(new PIDQuantityPair()
                        {
                            PID = pid
                        });
                    }

                    foreach (var jt in JArray.Parse(sale.Description).ToArray())
                    {
                        var code = int.Parse(jt["Code"].ToString());                        

                        if(code == req.Code)
                        {
                            var quantity = int.Parse(jt["Quantity"].ToString());

                            pqp.FirstOrDefault(pp => pp.PID == pid).Quantity += quantity;                            

                            break;
                        }                        
                    }
                }



                // Filter by search code and quantity
                pqp.RemoveAll(pp => pp.Quantity < req.MinQuantity);




                //  Get Consultants by PIDs with minimum quantity and code
                var pids = pqp.Select(p => p.PID).ToList();

                var consultants = _context.Consultants.Where(c => pids.Contains(c.PID)).ToList();



                //  Fill view model
                foreach (var con in consultants)
                {
                    vm.Items.Add(new SearchByFrequentlySoldProductsItem()
                    {
                        Birthdate = con.Birthdate,
                        Code = req.Code,
                        FullName = con.GetFullName(),
                        ID = con.ID,
                        PID = con.PID,
                        Quantity = pqp.FirstOrDefault(p => p.PID == con.PID).Quantity
                    });
                }

                vm.TotalQuantity = vm.Items.Select(i => i.Quantity).Sum();

            }
            else
            {
                ////  Get sales in Date range

                //var sales = _context.Sales.Where(s => s.SaleDate >= req.FromDate && s.SaleDate <= req.ToDate).ToList();



                ////  Get All Code-Quantity pairs among sales to detect most sold product

                //foreach (var sale in sales)
                //{
                //    foreach (var jt in JArray.Parse(sale.Description).ToArray())
                //    {
                //        var code = int.Parse(jt["Code"].ToString());
                //        var quantity = int.Parse(jt["Quantity"].ToString());

                //        var exists = pqp.FirstOrDefault(pp => pp.Code == code);

                //        if (exists != null)
                //        {
                //            exists.Quantity += quantity;
                //        }
                //        else
                //        {
                //            pqp.Add(new ProdQuantityPair()
                //            {
                //                Code = code,
                //                Quantity = quantity
                //            });
                //        }
                //    }

                //}


                ////  Filter products by quantity
                //pqp.RemoveAll(pp => pp.Quantity < req.MinQuantity || pp.Code != req.Code);

                //pqp.OrderBy(pp => pp.Quantity);




            }

            


            

            return View(vm);
        }



        ////კონსულტანტები ჯამური გაყიდვების მიხედვით:
        //[HttpGet, ActionName("SearchByFrequentlySoldProducts")]
        //public async Task<IActionResult> SearchByTotalSales()
        //{
        //    SearchByFrequentlySoldProductsViewModel vm = new SearchByFrequentlySoldProductsViewModel();
        //    return View(vm);
        //}




        //class ProdQuantityPair
        //{
        //    public int Code { get; set; }
        //    public int Quantity { get; set; }
        //}

        //class PIDQuantityPair
        //{
        //    public string PID { get; set; }
        //    public int Quantity { get; set; }
        //}

        ////კონსულტანტები ხშირად გაყიდვადი პროდუქტების მიხედვით:
        //[HttpPost, ActionName("SearchByFrequentlySoldProducts")]
        //public async Task<IActionResult> SearchByFrequentlySoldProducts(SearchByFrequentlySoldProductsRequest req)
        //{
        //    //  Include last day's sales
        //    req.ToDate = req.ToDate.AddHours(23).AddMinutes(59).AddSeconds(59);

        //    SearchByFrequentlySoldProductsViewModel vm = new SearchByFrequentlySoldProductsViewModel();



        //    var pqp = new List<PIDQuantityPair>();

        //    if (req.Code != 0)
        //    {
        //        //  Get sales in Date range

        //        var sales = _context.Sales.Where(s => s.SaleDate >= req.FromDate && s.SaleDate <= req.ToDate).ToList();



        //        //  Get All PID-Quantity pairs among sales

        //        foreach (var sale in sales)
        //        {
        //            var pid = JObject.Parse(sale.SellerInfo)["PID"].ToString();


        //            var exists = pqp.FirstOrDefault(pp => pp.PID == pid);

        //            if (exists == null)
        //            {
        //                pqp.Add(new PIDQuantityPair()
        //                {
        //                    PID = pid
        //                });
        //            }

        //            foreach (var jt in JArray.Parse(sale.Description).ToArray())
        //            {
        //                var code = int.Parse(jt["Code"].ToString());

        //                if (code == req.Code)
        //                {
        //                    var quantity = int.Parse(jt["Quantity"].ToString());

        //                    pqp.FirstOrDefault(pp => pp.PID == pid).Quantity += quantity;

        //                    break;
        //                }
        //            }
        //        }



        //        // Filter by search code and quantity
        //        pqp.RemoveAll(pp => pp.Quantity < req.MinQuantity);




        //        //  Get Consultants by PIDs with minimum quantity and code
        //        var pids = pqp.Select(p => p.PID).ToList();

        //        var consultants = _context.Consultants.Where(c => pids.Contains(c.PID)).ToList();



        //        //  Fill view model
        //        foreach (var con in consultants)
        //        {
        //            vm.Items.Add(new SearchByFrequentlySoldProductsItem()
        //            {
        //                Birthdate = con.Birthdate,
        //                Code = req.Code,
        //                FullName = con.GetFullName(),
        //                ID = con.ID,
        //                PID = con.PID,
        //                Quantity = pqp.FirstOrDefault(p => p.PID == con.PID).Quantity
        //            });
        //        }

        //        vm.TotalQuantity = vm.Items.Select(i => i.Quantity).Sum();

        //    }
        //    else
        //    {
        //        ////  Get sales in Date range

        //        //var sales = _context.Sales.Where(s => s.SaleDate >= req.FromDate && s.SaleDate <= req.ToDate).ToList();



        //        ////  Get All Code-Quantity pairs among sales to detect most sold product

        //        //foreach (var sale in sales)
        //        //{
        //        //    foreach (var jt in JArray.Parse(sale.Description).ToArray())
        //        //    {
        //        //        var code = int.Parse(jt["Code"].ToString());
        //        //        var quantity = int.Parse(jt["Quantity"].ToString());

        //        //        var exists = pqp.FirstOrDefault(pp => pp.Code == code);

        //        //        if (exists != null)
        //        //        {
        //        //            exists.Quantity += quantity;
        //        //        }
        //        //        else
        //        //        {
        //        //            pqp.Add(new ProdQuantityPair()
        //        //            {
        //        //                Code = code,
        //        //                Quantity = quantity
        //        //            });
        //        //        }
        //        //    }

        //        //}


        //        ////  Filter products by quantity
        //        //pqp.RemoveAll(pp => pp.Quantity < req.MinQuantity || pp.Code != req.Code);

        //        //pqp.OrderBy(pp => pp.Quantity);




        //    }






        //    return View(vm);
        //}


    }
}
