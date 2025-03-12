using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class problems1Controller : Controller
    {
        private readonly taskContext _context;

        public problems1Controller(taskContext context)
        {
            _context = context;
        }

        // GET: problems1
        public async Task<IActionResult> Index()
        {
            return View(await _context.problems1.ToListAsync());
        }

        // GET: problems1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problems1 = await _context.problems1
                .FirstOrDefaultAsync(m => m.PblmId == id);
            if (problems1 == null)
            {
                return NotFound();
            }

            return View(problems1);
        }

        // GET: problems1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: problems1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int PblmId,string  Difficulty, int SolvedBy,
          IFormFile TestCaseInput, IFormFile TestCaseOutput , double SuccessRate, 
         string Title, int TimeLimit,int  MemoryLimit,int WrongTry,int  WeightLimit,int Accepted)
        {



            if (TestCaseInput == null || TestCaseInput.Length == 0 || TestCaseOutput == null || TestCaseOutput.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            string fileContent, fileContent1;

            using (var stream = new StreamReader(TestCaseInput.OpenReadStream(), Encoding.UTF8))
            {
                // Read entire file as a string
                fileContent = await stream.ReadToEndAsync();
            }

            using (var stream = new StreamReader(TestCaseOutput.OpenReadStream(), Encoding.UTF8))
            {
                // Read entire file as a string
                fileContent1 = await stream.ReadToEndAsync();
            }


            // Trim any extra spaces and replace newlines with <br>
            string formattedContent = fileContent.Trim().Replace("\r\n", "<br>").Replace("\n", "<br>");
            string formattedContent1 = fileContent1.Trim().Replace("\r\n", "<br>").Replace("\n", "<br>");

            problems1 problems1 = new problems1();

            problems1.PblmId = PblmId;
            problems1.Difficulty = Difficulty;
            problems1.SolvedBy = SolvedBy;
            problems1.TestCaseInput = formattedContent;
            problems1.TestCaseOutput = formattedContent1;
            problems1.SuccessRate = SuccessRate;
            problems1.Title = Title;
            problems1.TimeLimit = TimeLimit;
            problems1.MemoryLimit = MemoryLimit;
            problems1.WrongTry = WrongTry;
            problems1.WeightLimit = WeightLimit;
            problems1.Accepted = Accepted;

          
                _context.Add(problems1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(problems1);
        }

        // GET: problems1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problems1 = await _context.problems1.FindAsync(id);
            if (problems1 == null)
            {
                return NotFound();
            }
            return View(problems1);
        }

        // POST: problems1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PblmId,Difficulty,SolvedBy,SuccessRate,Title,TimeLimit,MemoryLimit,WrongTry,WeightLimit,Accepted")] problems1 problems1)
        {
            if (id != problems1.PblmId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(problems1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!problems1Exists(problems1.PblmId))
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
            return View(problems1);
        }

        // GET: problems1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problems1 = await _context.problems1
                .FirstOrDefaultAsync(m => m.PblmId == id);
            if (problems1 == null)
            {
                return NotFound();
            }

            return View(problems1);
        }

        // POST: problems1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var problems1 = await _context.problems1.FindAsync(id);
            if (problems1 != null)
            {
                _context.problems1.Remove(problems1);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool problems1Exists(int id)
        {
            return _context.problems1.Any(e => e.PblmId == id);
        }
    }
}
