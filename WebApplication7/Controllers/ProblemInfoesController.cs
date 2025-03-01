using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class ProblemInfoesController : Controller
    {
        private readonly taskContext _context;

        public ProblemInfoesController(taskContext context)
        {
            _context = context;
        }

        // GET: ProblemInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProblemInfo.ToListAsync());
        }

        // GET: ProblemInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemInfo = await _context.ProblemInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problemInfo == null)
            {
                return NotFound();
            }

            return View(problemInfo);
        }

        // GET: ProblemInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProblemInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,InputFormat,OutputFormat,InputExample,OutputExample,Explanation")] ProblemInfo problemInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(problemInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(problemInfo);
        }

        // GET: ProblemInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemInfo = await _context.ProblemInfo.FindAsync(id);
            if (problemInfo == null)
            {
                return NotFound();
            }
            return View(problemInfo);
        }

        // POST: ProblemInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,InputFormat,OutputFormat,InputExample,OutputExample,Explanation")] ProblemInfo problemInfo)
        {
            if (id != problemInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(problemInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemInfoExists(problemInfo.Id))
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
            return View(problemInfo);
        }

        // GET: ProblemInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemInfo = await _context.ProblemInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problemInfo == null)
            {
                return NotFound();
            }

            return View(problemInfo);
        }

        // POST: ProblemInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var problemInfo = await _context.ProblemInfo.FindAsync(id);
            if (problemInfo != null)
            {
                _context.ProblemInfo.Remove(problemInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemInfoExists(int id)
        {
            return _context.ProblemInfo.Any(e => e.Id == id);
        }
    }
}
