using Mango.Services.Order.Web.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.Order.Web.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<OrderHeader> OrderHeaders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
