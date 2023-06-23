using Microsoft.EntityFrameworkCore;
using Mango.Services.ShoppingCart.Web.Api.Models;

namespace Mango.Services.ShoppingCart.Web.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<CartHeader> CartHeaders { get; set; }

        public DbSet<CartDetails> CartDetails { get; set; }
    }
}
