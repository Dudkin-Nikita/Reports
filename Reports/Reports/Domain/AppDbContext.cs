using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reports.Domain.Entities;
using Reports.Models;

namespace Reports.Domain
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User
            {
                LastSystemEnter = DateTime.UtcNow,
                UserType = UserTypes.Blocked,
                Id = "A6B399DC-1F54-448A-A267-CFA45D3FF03B",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "nikd926@gmail.com",
                NormalizedEmail = "NIKD926@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User?>().HashPassword(null, "qwertyu"),
                SecurityStamp = string.Empty
            });
        }

    }
}
