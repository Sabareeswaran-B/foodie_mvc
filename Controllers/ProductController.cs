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
    public class ProductController : Controller
    {
        private readonly foodieContext _context;

        public ProductController(foodieContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index(string Search)
        {
            var products = from pt in _context.Products select pt;

            if (!String.IsNullOrEmpty(Search))
            {
                products = products.Where(s => s.ProductName.Contains(Search));
            }

            return View(await products.ToListAsync());
        }

        // GET: Product/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Discount)
                .Include(p => p.Store)
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "DiscountId", "DiscountId");
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreId");
            ViewData["TypeId"] = new SelectList(_context.ProductTypes, "TypeId", "TypeId");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(
            [Bind(
                "ProductId,ProductName,QuantityAvailable,StoreId,TypeId,Mrp,SpecialPrice,Active,DiscountId"
            )]
                Product product
        )
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscountId"] = new SelectList(
                _context.Discounts,
                "DiscountId",
                "DiscountId",
                product.DiscountId
            );
            ViewData["StoreId"] = new SelectList(
                _context.Stores,
                "StoreId",
                "StoreId",
                product.StoreId
            );
            ViewData["TypeId"] = new SelectList(
                _context.ProductTypes,
                "TypeId",
                "TypeId",
                product.TypeId
            );
            return View(product);
        }

        // GET: Product/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["DiscountId"] = new SelectList(
                _context.Discounts,
                "DiscountId",
                "DiscountId",
                product.DiscountId
            );
            ViewData["StoreId"] = new SelectList(
                _context.Stores,
                "StoreId",
                "StoreId",
                product.StoreId
            );
            ViewData["TypeId"] = new SelectList(
                _context.ProductTypes,
                "TypeId",
                "TypeId",
                product.TypeId
            );
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(
            int id,
            [Bind(
                "ProductId,ProductName,QuantityAvailable,StoreId,TypeId,Mrp,SpecialPrice,Active,DiscountId"
            )]
                Product product
        )
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["DiscountId"] = new SelectList(
                _context.Discounts,
                "DiscountId",
                "DiscountId",
                product.DiscountId
            );
            ViewData["StoreId"] = new SelectList(
                _context.Stores,
                "StoreId",
                "StoreId",
                product.StoreId
            );
            ViewData["TypeId"] = new SelectList(
                _context.ProductTypes,
                "TypeId",
                "TypeId",
                product.TypeId
            );
            return View(product);
        }

        // GET: Product/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Discount)
                .Include(p => p.Store)
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
