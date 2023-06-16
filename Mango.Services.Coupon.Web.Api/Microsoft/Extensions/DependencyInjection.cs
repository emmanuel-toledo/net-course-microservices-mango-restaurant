using Microsoft.EntityFrameworkCore;
using Mango.Services.Coupon.Web.Api.Data;
using AutoMapper;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Mango.Services.Coupon.Web.Api.Microsoft.Extensions
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
            // Configure AutoMapper.
            services.ConfigureAutoMapper();
            // Configure Authentication and Authorization.
            services.ConfigureAuthentication(configuration);
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
        /// Function to configure the use of auto mapper with the "MappingConfig" class.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// Function to add the configuration to validate an access token to interact with this microservice.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        /// <param name="configuration">Application configuration.</param>
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration) 
        {
            var secret = configuration.GetValue<string>("ApiSettings:Secret");
            var issuer = configuration.GetValue<string>("ApiSettings:Issuer");
            var audience = configuration.GetValue<string>("ApiSettings:Audience");
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true,
                };
            });

            services.AddAuthorization();
        }
    }
}
