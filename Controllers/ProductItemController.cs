using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MallRAj.Data;
using MallRAj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MallRAj.Controllers
{
    public class ProductItemController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public ProductItemController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: ProductItem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductItems.Include(p => p.ProductType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //select top 1 * from ProductITem where Id === id
            //lamda expression
            var productItemModel = await _context.ProductItems
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productItemModel == null)
            {
                return NotFound();
            }

            return View(productItemModel);
        }

        // GET: ProductItem/Create
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Name");
            return View();
        }

        // POST: ProductItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductItemModel productItemModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if (file != null && file.Length > 0)
                {
                    fileName = file.FileName;
                    var path = Path.Combine(_env.WebRootPath,"uploads", file.FileName);

                    file.CopyTo(new FileStream(path, FileMode.Create));
                }
                productItemModel.ImagePath = fileName;
                _context.Add(productItemModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Name", productItemModel.ProductTypeId);
            return View(productItemModel);
        }

        // GET: ProductItem/Edit/5
        public async Task<IActionResult> Edit(int? id )
        {
            if (id == null)
            {
                return NotFound();
            }

            var productItemModel = await _context.ProductItems.FindAsync(id);
            if (productItemModel == null)
            {
                return NotFound();
            }

            


            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Name", productItemModel.ProductTypeId);
            return View(productItemModel);
        }

        // POST: ProductItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductItemModel productItemModel,IFormFile file)
        {
            if (id != productItemModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;
                    if(file != null && file.Length > 0)
                    {
                        fileName = file.FileName;
                        var path = Path.Combine(_env.WebRootPath, "uploads", file.FileName);
                        file.CopyTo(new FileStream(path, FileMode.Create));
                    }
                    productItemModel.ImagePath = fileName;
                    _context.Update(productItemModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductItemModelExists(productItemModel.Id))
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
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Id", productItemModel.ProductTypeId);
            return View(productItemModel);
        }

        // GET: ProductItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productItemModel = await _context.ProductItems
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productItemModel == null)
            {
                return NotFound();
            }

            return View(productItemModel);
        }

        // POST: ProductItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productItemModel = await _context.ProductItems.FindAsync(id);
            _context.ProductItems.Remove(productItemModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductItemModelExists(int id)
        {
            return _context.ProductItems.Any(e => e.Id == id);
        }
    }
}
