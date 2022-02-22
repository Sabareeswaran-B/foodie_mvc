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
    public class OrderStatusController : Controller
    {
        private readonly foodieContext _context;

        public OrderStatusController(foodieContext context)
        {
            _context = context;
        }

        // GET: OrderStatus
        public async Task<IActionResult> Index(string Search)
        {
            var status = from pt in _context.OrderStatuses select pt;

            if (!String.IsNullOrEmpty(Search))
            {
                status = status.Where(s => s.StatusName.Contains(Search));
            }

            return View(await status.ToListAsync());
        }

        // GET: OrderStatus/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatuses.FirstOrDefaultAsync(
                m => m.StatusId == id
            );
            if (orderStatus == null)
            {
                return NotFound();
            }

            return View(orderStatus);
        }

        // GET: OrderStatus/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(
            [Bind("StatusId,StatusName,Active")] OrderStatus orderStatus
        )
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderStatus);
        }

        // GET: OrderStatus/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatuses.FindAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }
            return View(orderStatus);
        }

        // POST: OrderStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("StatusId,StatusName,Active")] OrderStatus orderStatus
        )
        {
            if (id != orderStatus.StatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderStatusExists(orderStatus.StatusId))
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
            return View(orderStatus);
        }

        // GET: OrderStatus/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatuses.FirstOrDefaultAsync(
                m => m.StatusId == id
            );
            if (orderStatus == null)
            {
                return NotFound();
            }

            return View(orderStatus);
        }

        // POST: OrderStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderStatus = await _context.OrderStatuses.FindAsync(id);
            _context.OrderStatuses.Remove(orderStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool OrderStatusExists(int id)
        {
            return _context.OrderStatuses.Any(e => e.StatusId == id);
        }
    }
}
