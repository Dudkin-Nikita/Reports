using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reports.Domain.Entities;
using Reports.Models;
using System.Reflection.Emit;

namespace Reports.Domain
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}