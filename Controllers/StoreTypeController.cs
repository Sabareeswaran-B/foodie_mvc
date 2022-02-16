#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using foodie_mvc.Models;

namespace foodie_mvc.Controllers
{
    public class StoreTypeController : Controller
    {
        private readonly foodieContext _context;

        public StoreTypeController(foodieContext context)
        {
            _context = context;
        }

        // GET: StoreType
        public async Task<IActionResult> Index()
        {
            return View(await _context.StoreTypes.ToListAsync());
        }

        // GET: StoreType/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeType = await _context.StoreTypes
                .FirstOrDefaultAsync(m => m.StoreTypeId == id);
            if (storeType == null)
            {
                return NotFound();
            }

            return View(storeType);
        }

        // GET: StoreType/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoreType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("StoreTypeId,StoreType1,Active")] StoreType storeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storeType);
        }

        // GET: StoreType/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeType = await _context.StoreTypes.FindAsync(id);
            if (storeType == null)
            {
                return NotFound();
            }
            return View(storeType);
        }

        // POST: StoreType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("StoreTypeId,StoreType1,Active")] StoreType storeType)
        {
            if (id != storeType.StoreTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreTypeExists(storeType.StoreTypeId))
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
            return View(storeType);
        }

        // GET: StoreType/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeType = await _context.StoreTypes
                .FirstOrDefaultAsync(m => m.StoreTypeId == id);
            if (storeType == null)
            {
                return NotFound();
            }

            return View(storeType);
        }

        // POST: StoreType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storeType = await _context.StoreTypes.FindAsync(id);
            _context.StoreTypes.Remove(storeType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool StoreTypeExists(int id)
        {
            return _context.StoreTypes.Any(e => e.StoreTypeId == id);
        }
    }
}
