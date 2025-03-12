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
    public class RankViewModelsController : Controller
    {
        private readonly taskContext _context;

        public RankViewModelsController(taskContext context)
        {
            _context = context;
        }

        // GET: RankViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.RankViewModel.ToListAsync());
        }

        // GET: RankViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankViewModel = await _context.RankViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rankViewModel == null)
            {
                return NotFound();
            }

            return View(rankViewModel);
        }

        // GET: RankViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RankViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rank,UserId,Username,SolvedProblems,TotalRejected")] RankViewModel rankViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rankViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rankViewModel);
        }

        // GET: RankViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankViewModel = await _context.RankViewModel.FindAsync(id);
            if (rankViewModel == null)
            {
                return NotFound();
            }
            return View(rankViewModel);
        }

        // POST: RankViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rank,UserId,Username,SolvedProblems,TotalRejected")] RankViewModel rankViewModel)
        {
            if (id != rankViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rankViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankViewModelExists(rankViewModel.Id))
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
            return View(rankViewModel);
        }

        // GET: RankViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankViewModel = await _context.RankViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rankViewModel == null)
            {
                return NotFound();
            }

            return View(rankViewModel);
        }

        // POST: RankViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rankViewModel = await _context.RankViewModel.FindAsync(id);
            if (rankViewModel != null)
            {
                _context.RankViewModel.Remove(rankViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankViewModelExists(int id)
        {
            return _context.RankViewModel.Any(e => e.Id == id);
        }
    }
}
