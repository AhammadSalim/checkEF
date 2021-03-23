using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using checkEF.Models;

namespace checkEF.Controllers
{
    public class TblStudentDetailsController : Controller
    {
        private readonly HRSystemContext _context;

        public TblStudentDetailsController(HRSystemContext context)
        {
            _context = context;
        }

        // GET: TblStudentDetails
        public async Task<IActionResult> Index()
        {
            var hRSystemContext = _context.TblStudentDetails.Include(t => t.Grade);
            return View(await hRSystemContext.ToListAsync());
        }

        // GET: TblStudentDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblStudentDetails = await _context.TblStudentDetails
                .Include(t => t.Grade)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblStudentDetails == null)
            {
                return NotFound();
            }

            return View(tblStudentDetails);
        }

        // GET: TblStudentDetails/Create
        public IActionResult Create()
        {
            ViewData["GradeId"] = new SelectList(_context.TblGrade, "Id", "Id");
            return View();
        }

        // POST: TblStudentDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentName,DateOfBirth,Photo,Height,Weight,GradeId")] TblStudentDetails tblStudentDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblStudentDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradeId"] = new SelectList(_context.TblGrade, "Id", "Id", tblStudentDetails.GradeId);
            return View(tblStudentDetails);
        }

        // GET: TblStudentDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblStudentDetails = await _context.TblStudentDetails.FindAsync(id);
            if (tblStudentDetails == null)
            {
                return NotFound();
            }
            ViewData["GradeId"] = new SelectList(_context.TblGrade, "Id", "Id", tblStudentDetails.GradeId);
            return View(tblStudentDetails);
        }

        // POST: TblStudentDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentName,DateOfBirth,Photo,Height,Weight,GradeId")] TblStudentDetails tblStudentDetails)
        {
            if (id != tblStudentDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblStudentDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblStudentDetailsExists(tblStudentDetails.Id))
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
            ViewData["GradeId"] = new SelectList(_context.TblGrade, "Id", "Id", tblStudentDetails.GradeId);
            return View(tblStudentDetails);
        }

        // GET: TblStudentDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblStudentDetails = await _context.TblStudentDetails
                .Include(t => t.Grade)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblStudentDetails == null)
            {
                return NotFound();
            }

            return View(tblStudentDetails);
        }

        // POST: TblStudentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblStudentDetails = await _context.TblStudentDetails.FindAsync(id);
            _context.TblStudentDetails.Remove(tblStudentDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblStudentDetailsExists(int id)
        {
            return _context.TblStudentDetails.Any(e => e.Id == id);
        }
    }
}
