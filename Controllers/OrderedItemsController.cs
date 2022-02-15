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
    public class OrderedItemsController : Controller
    {
        private readonly foodieContext _context;

        public OrderedItemsController(foodieContext context)
        {
            _context = context;
        }

        // GET: OrderedItems
        public async Task<IActionResult> Index()
        {
            var foodieContext = _context.OrderedItems.Include(o => o.Order).Include(o => o.Product);
            return View(await foodieContext.ToListAsync());
        }

        // GET: OrderedItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var OrderedItems = await _context.OrderedItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (OrderedItems == null)
            {
                return NotFound();
            }

            return View(OrderedItems);
        }

        // GET: OrderedItems/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: OrderedItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,OrderId,ProductId,Quantity,Active")] OrderedItems OrderedItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(OrderedItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", OrderedItems.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", OrderedItems.ProductId);
            return View(OrderedItems);
        }

        // GET: OrderedItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var OrderedItems = await _context.OrderedItems.FindAsync(id);
            if (OrderedItems == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", OrderedItems.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", OrderedItems.ProductId);
            return View(OrderedItems);
        }

        // POST: OrderedItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,OrderId,ProductId,Quantity,Active")] OrderedItems OrderedItems)
        {
            if (id != OrderedItems.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(OrderedItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderedItemsExists(OrderedItems.ItemId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", OrderedItems.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", OrderedItems.ProductId);
            return View(OrderedItems);
        }

        // GET: OrderedItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var OrderedItems = await _context.OrderedItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (OrderedItems == null)
            {
                return NotFound();
            }

            return View(OrderedItems);
        }

        // POST: OrderedItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var OrderedItems = await _context.OrderedItems.FindAsync(id);
            _context.OrderedItems.Remove(OrderedItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderedItemsExists(int id)
        {
            return _context.OrderedItems.Any(e => e.ItemId == id);
        }
    }
}
