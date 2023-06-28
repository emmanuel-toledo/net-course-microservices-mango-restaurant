using Mango.Services.Email.Web.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.Email.Web.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<EmailLogger> EmailLoggers { get; set; }
    }
}
