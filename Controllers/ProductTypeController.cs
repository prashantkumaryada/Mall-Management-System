using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MallRAj;
using MallRAj.Data;

namespace MallRAj.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductType
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductTypes.ToListAsync());
        }

        // GET: ProductType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTypeModel = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productTypeModel == null)
            {
                return NotFound();
            }

            return View(productTypeModel);
        }

        // GET: ProductType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ProductTypeModel productTypeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productTypeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypeModel);
        }

        // GET: ProductType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTypeModel = await _context.ProductTypes.FindAsync(id);
            if (productTypeModel == null)
            {
                return NotFound();
            }
            return View(productTypeModel);
        }

        // POST: ProductType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ProductTypeModel productTypeModel)
        {
            if (id != productTypeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productTypeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeModelExists(productTypeModel.Id))
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
            return View(productTypeModel);
        }

        // GET: ProductType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTypeModel = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productTypeModel == null)
            {
                return NotFound();
            }

            return View(productTypeModel);
        }

        // POST: ProductType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productTypeModel = await _context.ProductTypes.FindAsync(id);
            _context.ProductTypes.Remove(productTypeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTypeModelExists(int id)
        {
            return _context.ProductTypes.Any(e => e.Id == id);
        }
    }
}
