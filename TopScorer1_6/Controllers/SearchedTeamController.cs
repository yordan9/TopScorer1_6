using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopScorer1_6.Models.MatchModels;
using TopScorer1_6.ViewModels;

namespace TopScorer1_6.Controllers
{
    public class SearchedTeamController : Controller
    {
        private readonly ApplicationDbContextMatches _context;

        public SearchedTeamController(ApplicationDbContextMatches context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query) //използва се за търсачката
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Index");
            }

            // Търсим отборите с това име
            var teams = await _context.Teams
                .Where(t => t.Name.Contains(query))
                .ToListAsync();

            if (!teams.Any())
            {
                return View("Index", new List<Team>());
            }

            var teamIds = teams.Select(t => t.Id).ToList();

            var matches = await _context.Matches //извличат се мачовете на отбора, който се търси 
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => teamIds.Contains(m.HomeTeamId) || teamIds.Contains(m.AwayTeamId))
                .Select(m => new MatchViewModel
                {
                    Id = m.Id,
                    MatchDate = m.MatchDate,
                    MatchTime = m.MatchTime,
                    HomeTeamId = m.HomeTeamId,
                    AwayTeamId = m.AwayTeamId,
                    HomeTeamName = m.HomeTeam.Name,
                    AwayTeamName = m.AwayTeam.Name,
                    FullTimeHomeGoal = m.FullTimeHomeGoal,
                    FullTimeAwayGoal = m.FullTimeAwayGoal
                })
                .ToListAsync();


            ViewBag.Matches = matches;
            ViewBag.SearchedTeamId = teams.FirstOrDefault()?.Id; // Взимаме Id-то на първия отбор от резултатите

            return View("Index", teams);
        }
    }
}
