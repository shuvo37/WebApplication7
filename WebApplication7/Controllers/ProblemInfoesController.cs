using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<IActionResult> Create(int Id ,string Description,string  InputFormat, 
            string OutputFormat, IFormFile InputExample, IFormFile OutputExample, string Explanation)
        {
            if (InputExample == null || InputExample.Length == 0 || OutputExample == null || OutputExample.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            string fileContent, fileContent1;

            using (var stream = new StreamReader(InputExample.OpenReadStream(), Encoding.UTF8))
            {
                // Read entire file as a string
                fileContent = await stream.ReadToEndAsync();
            }

            using (var stream = new StreamReader(OutputExample.OpenReadStream(), Encoding.UTF8))
            {
                // Read entire file as a string
                fileContent1 = await stream.ReadToEndAsync();
            }


            // Trim any extra spaces and replace newlines with <br>
            string formattedContent = fileContent.Trim().Replace("\r\n", "<br>").Replace("\n", "<br>");
            string formattedContent1 = fileContent1.Trim().Replace("\r\n", "<br>").Replace("\n", "<br>");
            //  [Bind("Id,Title,Description,InputFormat,OutputFormat,InputExample,OutputExample,Explanation")] ProblemInfo problemInfo
             ProblemInfo problemInfo = new ProblemInfo();

            var problems1 = await _context.problems1
                .FirstOrDefaultAsync(m => m.PblmId == Id);

              


            problemInfo.Id = Id;
            problemInfo.Title = problems1.Title;
            problemInfo.Description = Description;
            problemInfo.InputFormat = InputFormat;
            problemInfo.OutputFormat = OutputFormat;
            problemInfo.InputExample = formattedContent;
            problemInfo.OutputExample = formattedContent1;
            problemInfo.Explanation = Explanation;

            if (problems1 == null || Description == null || InputFormat == null ||
                OutputFormat == null || formattedContent == null || Explanation == null)
            {
                return View(problemInfo);
            }


            _context.Add(problemInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,InputFormat,OutputFormat,InputExample,OutputExample,Explanation")] ProblemInfo problemInfo)
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
