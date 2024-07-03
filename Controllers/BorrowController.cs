using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MallRAj.Data;
using MallRAj.Models;

namespace MallRAj.Controllers
{
    public class BorrowController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BorrowController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Borrow
        public async Task<IActionResult> Index()
        {
            return View(await _context.BorrowModel.ToListAsync());
        }

        // GET: Borrow/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowModel = await _context.BorrowModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowModel == null)
            {
                return NotFound();
            }

            return View(borrowModel);
        }

        // GET: Borrow/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Borrow/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerName,CustomerPhoneNumber,TotalAmount,Paid,Dues")] BorrowModel borrowModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(borrowModel);
        }

        // GET: Borrow/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowModel = await _context.BorrowModel.FindAsync(id);
            if (borrowModel == null)
            {
                return NotFound();
            }
            return View(borrowModel);
        }

        // POST: Borrow/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerName,CustomerPhoneNumber,TotalAmount,Paid,Dues")] BorrowModel borrowModel)
        {
            if (id != borrowModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowModelExists(borrowModel.Id))
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
            return View(borrowModel);
        }

        // GET: Borrow/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowModel = await _context.BorrowModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowModel == null)
            {
                return NotFound();
            }

            return View(borrowModel);
        }

        // POST: Borrow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowModel = await _context.BorrowModel.FindAsync(id);
            _context.BorrowModel.Remove(borrowModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowModelExists(int id)
        {
            return _context.BorrowModel.Any(e => e.Id == id);
        }
    }
}
