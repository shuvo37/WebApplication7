using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication7.Data;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class Pblm_viewController : Controller
    {
        private readonly taskContext _context;

    

        public Pblm_viewController(taskContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<problems1> problemsList = _context.problems1.ToList();
            return View(problemsList);
        }

        

        [HttpGet]
        public async Task<IActionResult> ProblemDescription(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Correct way to query the database
            var problemdes = await _context.ProblemInfo.FirstOrDefaultAsync(p => p.Id == id);

            if (problemdes == null)
            {
                return NotFound();
            }

            return View(problemdes);
        }


    }
}
