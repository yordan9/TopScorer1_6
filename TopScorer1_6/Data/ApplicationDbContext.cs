using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TopScorer1_6.Models.IdentityModels;

namespace TopScorer1_6.Data
{
    //database for admin and subscribers
    public class ApplicationDbContext : IdentityDbContext
    {
       public DbSet<Subscriber> Subscribers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //adminN pass AdminA
            //не работи
            //Създаване на admin акаунт
            base.OnModelCreating(modelBuilder);

            var hasher = new PasswordHasher<IdentityUser>();
            var admin = new IdentityUser
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "Admin1",
                NormalizedUserName = "ADMIN1@MAIL.COM",
                Email = "admin1@mail.com",
                NormalizedEmail = "ADMIN1@MAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            admin.PasswordHash =
                hasher.HashPassword(admin, "admin123");
            modelBuilder.Entity<IdentityUser>().HasData(admin);
        }
    }
}
