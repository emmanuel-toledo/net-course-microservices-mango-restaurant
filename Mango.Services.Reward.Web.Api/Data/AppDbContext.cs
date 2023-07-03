using Microsoft.EntityFrameworkCore;

namespace Mango.Services.Reward.Web.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<Models.Rewards> Rewards { get; set; }
    }
}
