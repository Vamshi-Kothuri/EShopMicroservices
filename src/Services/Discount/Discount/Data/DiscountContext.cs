using Discount.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                 new Coupon { Id = 1, ProductName = "IPhone", Description = "Apple Series", Amount = 5000},
                 new Coupon { Id = 2, ProductName = "Samsung", Description = "Samsung Series", Amount = 3000}
                );
        }

        public DbSet<Coupon> Coupons { get; set; } = default!;
    }
}
