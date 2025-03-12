using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class RankController : Controller
    {

     
        private readonly taskContext _context;

        public RankController(taskContext context)
        {
           
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var rankings = await _context.Submission
              .Where(s => s.UserId != null)
              .GroupBy(s => new { s.UserId, s.Username })
              .Select(g => new
              {
                  UserId = g.Key.UserId,
                  Username = g.Key.Username,
                  SolvedProblems = g.Where(x => x.Result == "Accepted").Select(x => x.Pblm_id).Distinct().Count(),
                  TotalRejected = g.Count(x => x.Result == "Rejected")
              })
              .OrderByDescending(r => r.SolvedProblems) // Highest solved problems first
              .ThenBy(r => r.TotalRejected) // Lowest rejected count first
              .ThenBy(r => r.UserId) // Lower UserId gets higher rank in case of ties
              .ToListAsync();

            var rankList = rankings
                .Select((r, index) => new RankViewModel
                {
                    Rank = index + 1,
                    UserId = r.UserId.Value,
                    Username = r.Username,
                    SolvedProblems = r.SolvedProblems,
                    TotalRejected = r.TotalRejected
                }).ToList();

           
            return View(rankList);
        }
    }
}
