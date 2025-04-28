using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TopScorer1_6.Models;
using TopScorer1_6.Models.MatchModels;
using TopScorer1_6.ViewModels;


namespace TopScorer1_6.Controllers
{
    public class FixtureController : Controller
    {
        private readonly ApplicationDbContextMatches _context;

        public FixtureController(ApplicationDbContextMatches context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> GetFixturesByLeague(string league) //извлича мачовете по лига, според зависи каква лига е избрана
        {
            var matches = await _context.Matches
                .AsNoTracking() //предотвратява дългото зареждане
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => m.League.Name == league)
                .Select(m => new FullInfoMatchViewModel //използваме специално направен viewmodel
                {
                    Id = m.Id,
                    HomeTeam = m.HomeTeam.Name,
                    AwayTeam = m.AwayTeam.Name,
                    MatchDate = m.MatchDate,
                    FTHG = m.FullTimeHomeGoal,
                    FTAG = m.FullTimeAwayGoal
                })
                .ToListAsync();

            return PartialView("Index", matches);
        }

        [HttpPost]
        public async Task<IActionResult> GetFixturesByDate([FromBody] DateOnly date) //извлича мачовете по дата, според зависи каква е избрана
        {
            var matches = await _context.Matches
                .AsNoTracking()
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => m.MatchDate == date)
                .Select(m => new FullInfoMatchViewModel //използваме специално направен viewmodel
                {
                    Id = m.Id,
                    HomeTeam = m.HomeTeam.Name,
                    AwayTeam = m.AwayTeam.Name,
                    MatchDate = m.MatchDate,
                    FTHG = m.FullTimeHomeGoal,
                    FTAG = m.FullTimeAwayGoal
                })
                .ToListAsync();

            return PartialView("Index", matches);
        }

        //извлича информацията от API, с външен ключ
        private readonly string _apiKey = "295d56477d264f74b4b7e3e93f2d6373";

        public async Task<IActionResult> ChampionsLeagueMatches()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Auth-Token", _apiKey);

            var url = "https://api.football-data.org/v4/competitions/CL/matches";
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ChampionsLeagueResponse>(json);

            var matches = data.Matches.Select(match => new 
            {
                Id = match.Id,
                MatchDate = DateTime.Parse(match.UtcDate.ToString()).ToLocalTime().ToString("dd.MM.yyyy HH:mm"),
                HomeTeam = match.HomeTeam.Name,
                AwayTeam = match.AwayTeam.Name,
                FTHG = match.Score.FullTime.Home ?? 0,
                FTAG = match.Score.FullTime.Away ?? 0
            }).ToList<dynamic>();

            return PartialView("Api", matches);
        }

        public async Task<IActionResult> TodayMatches()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Auth-Token", _apiKey);

            var today = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var url = $"https://api.football-data.org/v4/matches?dateFrom={today}&dateTo={today}";

            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return PartialView("TodayMatchesPartial", new List<APIFixture>());
            }

            var data = JsonConvert.DeserializeObject<FixturesResponse>(json);

            foreach (var match in data.Matches)
            {
                //match.LocalDateTime = TimeZoneInfo.ConvertTimeFromUtc(
                //    DateTime.Parse(match.UtcDate),
                //    TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time")
                //);

                //try
                //{
                //    // Уверяваме се, че времето е в UTC, като задаваме "DateTimeKind.Utc"
                //    var utcDate = DateTime.Parse(match.UtcDate, null, System.Globalization.DateTimeStyles.AssumeUniversal);
                //    match.LocalDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
                //}
                //catch (Exception ex)
                //{
                //    // Логваме грешката, ако има проблем със стойността на времето
                //    Console.WriteLine($"Time Zone Conversion Error: {ex.Message}");
                //}

                match.LocalDateTime = DateTime.Parse(match.UtcDate, null, System.Globalization.DateTimeStyles.AssumeUniversal);
            }

            return PartialView("TodayMatchesPartial", data.Matches);
        }

        public IActionResult MatchGrid(string league) //създава grid table, използва инфо от базата, според зависи лигата
        {
            var matches = _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => m.League.Name == league)
                .ToList();

            var teams = matches.Select(m => m.HomeTeam.Name)
                               .Union(matches.Select(m => m.AwayTeam.Name))
                               .Distinct()
                               .OrderBy(t => t) // По азбучен ред
                               .ToList();

            var results = new Dictionary<(string, string), string>();

            foreach (var match in matches)
            {
                var home = match.HomeTeam.Name;
                var away = match.AwayTeam.Name;
                results[(home, away)] = $"{match.FullTimeHomeGoal} {match.FullTimeAwayGoal}";
            }

            var teamAbbreviations = teams.ToDictionary(
                t => t,
                t => new string(t.Where(char.IsLetter).Take(3).ToArray()).ToUpper()
            );

            var model = new MatchGridViewModel
            {
                Teams = teams,
                Results = results,
                TeamAbbreviations = teamAbbreviations
            };

            return PartialView("MatchGridTable", model);
        }

    }
}
