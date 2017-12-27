using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using investrs.Data;
using investrs.Models;

namespace investrs.Controllers
{
    public class AdminItemPriceHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminItemPriceHistoryController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: AdminItemPriceHistory
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ItemPriceHistory.Include(i => i.Item);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdminItemPriceHistory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPriceHistory = await _context.ItemPriceHistory
                .Include(i => i.Item)
                .SingleOrDefaultAsync(m => m.ItemPriceHistoryID == id);
            if (itemPriceHistory == null)
            {
                return NotFound();
            }

            return View(itemPriceHistory);
        }

        // GET: AdminItemPriceHistory/Create
        public IActionResult Create()
        {
            ViewData["ItemID"] = new SelectList(_context.Item, "ItemID", "ItemID");
            return View();
        }

        // POST: AdminItemPriceHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemPriceHistoryID,ItemID,Date,Price,AveragePrice")] ItemPriceHistory itemPriceHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemPriceHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ItemID"] = new SelectList(_context.Item, "ItemID", "ItemID", itemPriceHistory.ItemID);
            return View(itemPriceHistory);
        }

        // GET: AdminItemPriceHistory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPriceHistory = await _context.ItemPriceHistory.SingleOrDefaultAsync(m => m.ItemPriceHistoryID == id);
            if (itemPriceHistory == null)
            {
                return NotFound();
            }
            ViewData["ItemID"] = new SelectList(_context.Item, "ItemID", "ItemID", itemPriceHistory.ItemID);
            return View(itemPriceHistory);
        }

        // POST: AdminItemPriceHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemPriceHistoryID,ItemID,Date,Price,AveragePrice")] ItemPriceHistory itemPriceHistory)
        {
            if (id != itemPriceHistory.ItemPriceHistoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemPriceHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemPriceHistoryExists(itemPriceHistory.ItemPriceHistoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ItemID"] = new SelectList(_context.Item, "ItemID", "ItemID", itemPriceHistory.ItemID);
            return View(itemPriceHistory);
        }

        // GET: AdminItemPriceHistory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPriceHistory = await _context.ItemPriceHistory
                .Include(i => i.Item)
                .SingleOrDefaultAsync(m => m.ItemPriceHistoryID == id);
            if (itemPriceHistory == null)
            {
                return NotFound();
            }

            return View(itemPriceHistory);
        }

        // POST: AdminItemPriceHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemPriceHistory = await _context.ItemPriceHistory.SingleOrDefaultAsync(m => m.ItemPriceHistoryID == id);
            _context.ItemPriceHistory.Remove(itemPriceHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ItemPriceHistoryExists(int id)
        {
            return _context.ItemPriceHistory.Any(e => e.ItemPriceHistoryID == id);
        }
    }
}
