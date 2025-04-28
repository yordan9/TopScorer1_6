using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopScorer1_6.Models.MatchModels;

namespace TopScorer1_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContextMatches _context;

        public SearchController(ApplicationDbContextMatches context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuggestions(string query) //дава предположения, като взема написаното в търсачката
        {

            var teams = await _context.Teams
                .AsNoTracking()
                .Where(t => t.Name.StartsWith(query)) // по-бързо от Contains
                .Select(t => new
                {
                    id = t.Id,
                    name = t.Name
                })
                .Take(10) // ограничаваме броя
                .ToListAsync();

                return Ok(teams);
        }
    }
}
