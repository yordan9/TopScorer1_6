using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopScorer1_6.Models.MatchModels;
using TopScorer1_6.ViewModels;

namespace TopScorer1_6.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly ApplicationDbContextMatches _context;

        public AdminPanelController(ApplicationDbContextMatches context)
        {
            _context = context;
        }

        //метод за записване на нов мач в базата
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMatch(AdminPanelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var match = new Match //полетата на таблицата Match
                {
                    LeagueId = model.LeagueId,
                    MatchDate = DateOnly.Parse(model.MatchDate),
                    MatchTime = TimeOnly.Parse(model.MatchTime),
                    HomeTeamId = model.HomeTeamId,
                    AwayTeamId = model.AwayTeamId,
                    HalfTimeHomeGoal = model.HalfTimeHomeGoal,
                    HalfTimeAwayGoal = model.HalfTimeAwayGoal,
                    HalfTimeResultId = model.HalfTimeResultId,
                    FullTimeHomeGoal = model.FullTimeHomeGoal,
                    FullTimeAwayGoal = model.FullTimeAwayGoal,
                    FullTimeResultId = model.FullTimeResultId,
                    RefereeId = model.RefereeId,
                    HomeShot = model.HomeShot,
                    AwayShot = model.AwayShot,
                    HomeShotTarget = model.HomeShotTarget,
                    AwayShotTarget = model.AwayShotTarget,
                    HomeFoul = model.HomeFoul,
                    AwayFoul = model.AwayFoul,
                    HomeCorner = model.HomeCorner,
                    AwayCorner = model.AwayCorner,
                    HomeYellowCard = model.HomeYellowCard,
                    AwayYellowCard = model.AwayYellowCard,
                    HomeRedCard = model.HomeRedCard,
                    AwayRedCard = model.AwayRedCard
                };

                _context.Matches.Add(match);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Мачът беше добавен успешно!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Грешка при добавянето. Провери въведените данни.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetTeamsByLeague(int leagueId) //показва отборите от избраната преди това лига
        {
            var teams = _context.TeamsLeagues
                .Include(t => t.Team)
                .Where(t => t.LeagueId == leagueId)
                .Select(t => new {
                    teamId = t.Team.Id,
                    teamName = t.Team.Name
                })
                .Distinct()
                .ToList();

            return Json(teams);
        }

        [HttpGet]
        public JsonResult GetReferees() //показва съдиите
        {
            var referee = _context.Referees
                .Select(r => new
                {
                    refereeId = r.Id,
                    refereeName = r.Name
                })
                .Distinct() //ако има потварящи се рефери
                 .ToList();

            return Json(referee);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
