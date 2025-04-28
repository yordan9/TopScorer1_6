using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using TopScorer1_6.Models;
using TopScorer1_6.Models.MatchModels;
using TopScorer1_6.ViewModels;

namespace TopScorer1_6.Controllers
{
    public class FullMatchInfoController : Controller
    {
        private readonly ApplicationDbContextMatches _context;

        public FullMatchInfoController(ApplicationDbContextMatches context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ShowFullInfo(int id) //показва пълното инфо на мача по id
        {
            var match =  await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Referee)
                .Include(m => m.FullTimeResult)
                .Include(m => m.HalfTimeResult)
                .Include(m => m.League)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null)
            {
                return NotFound(); //ако мача е null
            }

            var fullInfo = new FullInfoMatchViewModel //отделно направен viewmodel, не директно с Match, която е от базата
            {
                LeagueName = match.League?.Name ?? "Unknown league",
                HomeTeam = match.HomeTeam?.Name ?? "Unknown host",
                AwayTeam = match.AwayTeam?.Name ?? "Unknown guest",
                MatchTime = match.MatchTime,
                MatchDate = match.MatchDate,
                FTHG = match.FullTimeHomeGoal,
                FTAG = match.FullTimeAwayGoal,
                FTLetter = match.FullTimeResult?.Letter ?? "No data",
                HTHG = match.HalfTimeHomeGoal ?? 0,
                HTAG = match.HalfTimeAwayGoal ?? 0,
                HTLetter = match.HalfTimeResult?.Letter ?? "No data",
                Referee = match.Referee?.Name ?? "No data",
                HomeShot = match.HomeShot ?? 0,
                AwayShot = match.AwayShot ?? 0,
                HomeShotTarget = match.HomeShotTarget ?? 0,
                AwayShotTarget = match.AwayShotTarget ?? 0,
                HomeFoul = match.HomeFoul ?? 0,
                AwayFoul = match.AwayFoul ?? 0,
                HomeCorner = match.HomeCorner ?? 0,
                AwayCorner = match.AwayCorner ?? 0,
                HomeYellowCard = match.HomeYellowCard ?? 0,
                AwayYellowCard = match.AwayYellowCard ?? 0,
                HomeRedCard = match.HomeRedCard ?? 0,
                AwayRedCard = match.AwayRedCard ?? 0
            };

            return View("Index", fullInfo);
        }

        public async Task<IActionResult> ShowNoInfo() //не се извлича пълно инфо за мачовете от API-то, просто се връща view
        {
            return View("NoInfo");
        }
    }
}
