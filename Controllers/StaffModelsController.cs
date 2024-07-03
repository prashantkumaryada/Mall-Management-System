﻿using System;
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
    public class StaffModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StaffModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.StaffModel.ToListAsync());
        }

        // GET: StaffModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffModel = await _context.StaffModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffModel == null)
            {
                return NotFound();
            }

            return View(staffModel);
        }

        // GET: StaffModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StaffModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StaffName,MobileNumber,Address,Salary")] StaffModel staffModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staffModel);
        }

        // GET: StaffModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffModel = await _context.StaffModel.FindAsync(id);
            if (staffModel == null)
            {
                return NotFound();
            }
            return View(staffModel);
        }

        // POST: StaffModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StaffName,MobileNumber,Address,Salary")] StaffModel staffModel)
        {
            if (id != staffModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffModelExists(staffModel.Id))
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
            return View(staffModel);
        }

        // GET: StaffModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffModel = await _context.StaffModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffModel == null)
            {
                return NotFound();
            }

            return View(staffModel);
        }

        // POST: StaffModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffModel = await _context.StaffModel.FindAsync(id);
            _context.StaffModel.Remove(staffModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffModelExists(int id)
        {
            return _context.StaffModel.Any(e => e.Id == id);
        }
    }
}
