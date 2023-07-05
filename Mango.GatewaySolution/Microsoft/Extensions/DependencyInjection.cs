using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Mango.GatewaySolution.Microsoft.Extensions
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
			// Add use of OCELOT Gateway.
			services.AddOcelot();

			// Configure Authentication and Authorization to validate JWT from AuthAPI.
			services.ConfigureAuthentication(configuration);
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
        /// Main function to apply any pending migration and execute the application.
        /// </summary>
        /// <param name="app">Web application object.</param>
        public static async Task RunApplicationAsync(this WebApplication app)
        {
			// Add use of OCELOT Gateway.
			await app.UseOcelot();
			// Execute application.
			app.Run();
        }
    }
}
