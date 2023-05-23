using Microsoft.EntityFrameworkCore;

namespace Mango.ShoppingCart.Web.Api.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
