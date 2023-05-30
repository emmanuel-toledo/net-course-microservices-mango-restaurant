using Microsoft.EntityFrameworkCore;
using Mango.Services.Coupon.Web.Api.Models;

namespace Mango.Services.Coupon.Web.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add data to the database for Coupons.
            modelBuilder.Entity<Models.Coupon>().HasData(new Models.Coupon()
            {
                CouponId = 1,
                CouponCode = "10OFF",
                DiscountAmount = 10,
                MinAmount = 20,
            });

            modelBuilder.Entity<Models.Coupon>().HasData(new Models.Coupon()
            {
                CouponId = 2,
                CouponCode = "20OFF",
                DiscountAmount = 20,
                MinAmount = 40,
            });
        }

        public DbSet<Models.Coupon> Coupons { get; set; }
    }
}
