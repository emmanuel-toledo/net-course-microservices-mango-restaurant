using Microsoft.EntityFrameworkCore;
using Mango.Services.Coupon.Web.Api.Models;

namespace Mango.Services.Coupon.Web.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<Models.Coupon> Coupons { get; set; }
    }
}
