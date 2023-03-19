using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinTucCongNghe.Models;

namespace TinTucCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAdvsController : Controller
    {
        private readonly dbTinTucCongNgheContext _context;

        public AdminAdvsController(dbTinTucCongNgheContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminAdvs
        public async Task<IActionResult> Index()
        {
              return _context.Advs != null ? 
                          View(await _context.Advs.ToListAsync()) :
                          Problem("Entity set 'dbTinTucCongNgheContext.Advs'  is null.");
        }

        // GET: Admin/AdminAdvs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Advs == null)
            {
                return NotFound();
            }

            var adv = await _context.Advs
                .FirstOrDefaultAsync(m => m.AdversitingId == id);
            if (adv == null)
            {
                return NotFound();
            }

            return View(adv);
        }

        // GET: Admin/AdminAdvs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminAdvs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdversitingId,Title,UrlLink,IsActive,DateCreated,Thumb,Img1,Img2")] Adv adv)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adv);
        }

        // GET: Admin/AdminAdvs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Advs == null)
            {
                return NotFound();
            }

            var adv = await _context.Advs.FindAsync(id);
            if (adv == null)
            {
                return NotFound();
            }
            return View(adv);
        }

        // POST: Admin/AdminAdvs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdversitingId,Title,UrlLink,IsActive,DateCreated,Thumb,Img1,Img2")] Adv adv)
        {
            if (id != adv.AdversitingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adv);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvExists(adv.AdversitingId))
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
            return View(adv);
        }

        // GET: Admin/AdminAdvs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Advs == null)
            {
                return NotFound();
            }

            var adv = await _context.Advs
                .FirstOrDefaultAsync(m => m.AdversitingId == id);
            if (adv == null)
            {
                return NotFound();
            }

            return View(adv);
        }

        // POST: Admin/AdminAdvs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Advs == null)
            {
                return Problem("Entity set 'dbTinTucCongNgheContext.Advs'  is null.");
            }
            var adv = await _context.Advs.FindAsync(id);
            if (adv != null)
            {
                _context.Advs.Remove(adv);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvExists(int id)
        {
          return (_context.Advs?.Any(e => e.AdversitingId == id)).GetValueOrDefault();
        }
    }
}
