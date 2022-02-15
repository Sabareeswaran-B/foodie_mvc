#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using foodie_mvc.Models;

namespace foodie_mvc.Controllers
{
    public class StoreController : Controller
    {
        private readonly foodieContext _context;

        public StoreController(foodieContext context)
        {
            _context = context;
        }

        // GET: Store
        public async Task<IActionResult> Index(string Search)
        {
            var stores = from pt in _context.Stores select pt;

            if (!String.IsNullOrEmpty(Search))
            {
                stores = stores.Where(s => s.StoreName.Contains(Search) || s.OwnerName.Contains(Search));
            }

            return View(await stores.ToListAsync());
        }

        // GET: Store/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .Include(s => s.Admin)
                .Include(s => s.StoreTypeNavigation)
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Store/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Users, "UserId", "UserId");
            ViewData["StoreType"] = new SelectList(_context.StoreTypes, "StoreTypeId", "StoreTypeId");
            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,OwnerName,StoreAddress,AdminId,Active,StoreName,StoreType")] Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Users, "UserId", "UserId", store.AdminId);
            ViewData["StoreType"] = new SelectList(_context.StoreTypes, "StoreTypeId", "StoreTypeId", store.StoreType);
            return View(store);
        }

        // GET: Store/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Users, "UserId", "UserId", store.AdminId);
            ViewData["StoreType"] = new SelectList(_context.StoreTypes, "StoreTypeId", "StoreTypeId", store.StoreType);
            return View(store);
        }

        // POST: Store/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StoreId,OwnerName,StoreAddress,AdminId,Active,StoreName,StoreType")] Store store)
        {
            if (id != store.StoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.StoreId))
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
            ViewData["AdminId"] = new SelectList(_context.Users, "UserId", "UserId", store.AdminId);
            ViewData["StoreType"] = new SelectList(_context.StoreTypes, "StoreTypeId", "StoreTypeId", store.StoreType);
            return View(store);
        }

        // GET: Store/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .Include(s => s.Admin)
                .Include(s => s.StoreTypeNavigation)
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.StoreId == id);
        }
    }
}
