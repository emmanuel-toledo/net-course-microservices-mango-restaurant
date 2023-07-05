using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Mango.Services.Product.Web.Api.Data;
using Microsoft.OpenApi.Models;

namespace Mango.Services.Product.Web.Api.Microsoft.Extensions
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
            // Configure swagger in the application.
            services.ConfigureSwaggerDoc();
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

        /// <summary>
        /// Function to apply all the configuration to swagger.
        /// <para>
        /// It includes the authentication for this web api.
        /// </para>
        /// </summary>
        /// <param name="services">Application service collection.</param>
        public static void ConfigureSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                // Configure swagger to accept authentication token.
                option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer { Generated access token }`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[]{ }
                    }
                });
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
            // Configure the use of static files as images in "wwwroot" folder.
            app.UseStaticFiles();
			// Execute application.
			app.Run();
		}
	}
}
