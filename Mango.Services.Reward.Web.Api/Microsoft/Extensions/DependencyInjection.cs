using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mango.Services.Reward.Web.Api.Data;
using Mango.Services.Reward.Web.Api.Messaging;
using Mango.Services.Reward.Web.Api.Services;

namespace Mango.Services.Reward.Web.Api.Microsoft.Extensions
{
    /// <summary>
    /// This class works as a container of all extension methods for service collection of the application.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// This function works as main method to setup all the configuration of the web api.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        /// <param name="configuration">Application configuration.</param>
        public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure SQL Server connection.
            services.ConfigureDbContext(configuration);
            // Configure Service Bus Consumer.
            services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();

            // Configure Email Service. We do the following becasue we are using Singleton services and DbContext is Scope service.
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            services.AddSingleton(new RewardService(optionBuilder.Options));
        }

        /// <summary>
        /// Function to configure SQL Server using Entity Framework Core.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        /// <param name="configuration">Application configuration.</param>
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        /// <summary>
        /// Main function to apply any pending migration and execute the application.
        /// </summary>
        /// <param name="app">Web application object.</param>
        public static void RunApplication(this WebApplication app)
        {
            // Apply any pending migration to the database.
            using (var scope = app.Services.CreateScope())
            {
                // Get "AppDbContext" service.
                var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // Validate if are pending migrations in the database.
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    // Apply the pendings migrations in the database (it is like execute update-database command).
                    _db.Database.Migrate();
                }
            }
            // Configure in the Pipeline the use of IAzureServiceBusConsumer service.
            app.UseAzureServiceBusConsumer();
            // Execute application.
            app.Run();
        }
    }
}
