using Mango.Integration.MessageBus;
using Mango.Services.Auth.Web.Api.Data;
using Mango.Services.Auth.Web.Api.Models;
using Mango.Services.Auth.Web.Api.Service;
using Mango.Services.Auth.Web.Api.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.Auth.Web.Api.Microsoft.Extensions
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
            // Configure Identity use.
            services.ConfigureIdentity(configuration);
            // Configure the JwtOptions with configuration information.
            services.Configure<JwtOptions>(configuration.GetSection("ApiSettings:JwtOptions"));
            // Register custom services.
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAuthService, AuthService>();
            // Register Message Bus service to interact with Azure Service Bus.
            services.AddScoped<IMessageBus, MessageBus>();
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
        /// Function to configure the use of Identity in our application.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        /// <param name="configuration">Application configuration.</param>
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        }
    }
}
