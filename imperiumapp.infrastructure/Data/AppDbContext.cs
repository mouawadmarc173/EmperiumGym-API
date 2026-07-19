using Microsoft.EntityFrameworkCore;
using imperiumapp.Core.Entities;

namespace imperiumapp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        // هيدا الـ Constructor بياخد إعدادات الاتصال بالداتا بيز
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // هون عم نخبر السيستم يحول الكلاسات اللي عملناها لجداول فعلية بالداتا بيز
        public DbSet<Member> Members { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SaleTransaction> SaleTransactions { get; set; }
        
    }
}