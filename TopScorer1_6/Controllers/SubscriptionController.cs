using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopScorer1_6.Data;
using TopScorer1_6.Models.IdentityModels;

namespace TopScorer1_6.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        //метод, който записва събовете в базата
        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            // Провери дали вече има такъв имейл в базата
            var existingSubscriber = await _context.Subscribers
                .SingleOrDefaultAsync(s => s.Email == email);

            if (existingSubscriber != null)
            {
                TempData["ErrorMessage"] = "This email is already subscribed!";
                return RedirectToAction("Index", "Home");
            }

            // Добавяне на нов абонат
            var subscriber = new Subscriber
            {
                Email = email,
                SubscriptionDate = DateTime.Now
            };

            //запазваме абоната в базата
            _context.Subscribers.Add(subscriber);
            _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "You have successfully subscribed!";
            return RedirectToAction("Index", "Home");
        }
    }
}
