using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MgebroStore.Models;
using MgebroStore.Models.Entities;

namespace MgebroStore.Controllers
{
    public class ConsultantsController : Controller
    {
        private readonly StoreContext _context;

        public ConsultantsController(StoreContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            return View(await _context.Consultants.ToListAsync());
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultants
                .FirstOrDefaultAsync(m => m.ID == id);
            if (consultant == null)
            {
                return NotFound();
            }

            return View(consultant);
        }



        public IActionResult Create()
        {

            List<SelectListItem> refDict = new List<SelectListItem>();

            refDict.Add(new SelectListItem("არავინ", "0"));

            refDict.AddRange(_context.Consultants.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.LastName + " " + c.FirstName }).ToList());

            ViewBag.Referrals = refDict;

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,PID,Gender,Birthdate,Referral")] Consultant consultant)
        {
            if (consultant.Referral != 0) {
                var exists = _context.Consultants.FirstOrDefault(c => c.ID == consultant.Referral);
                if(exists is null)
                    return View(consultant);
            }

            if (ModelState.IsValid)
            {
                _context.Add(consultant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consultant);
        }

        // GET: Consultants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultants.FindAsync(id);
            if (consultant == null)
            {
                return NotFound();
            }
            return View(consultant);
        }

        // POST: Consultants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,PID,Gender,Birthdate,Referral")] Consultant consultant)
        {
            if (id != consultant.ID)
            {
                return NotFound();
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

        // GET: Consultants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultants
                .FirstOrDefaultAsync(m => m.ID == id);
            if (consultant == null)
            {
                return NotFound();
            }

            return View(consultant);
        }

        // POST: Consultants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultant = await _context.Consultants.FindAsync(id);
            _context.Consultants.Remove(consultant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultantExists(int id)
        {
            return _context.Consultants.Any(e => e.ID == id);
        }
    }
}
