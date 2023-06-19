using Mango.Web.App.Service;
using Mango.Web.App.Service.IService;
using Mango.Web.App.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Mango.Web.App.Microsoft.Extensions
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
            // Configure main services to use HTTP services in the application.
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddHttpClient<ICouponService, CouponService>();
            services.AddHttpClient<IAuthService, AuthService>();
            services.AddHttpClient<IProductService, ProductService>();

            // Seed services urls for the application.
            services.SeedServicesUrls(configuration);

            // Configure services in the application.
            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IProductService, ProductService>();

            // Configure the authentications.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(10);
                options.LoginPath = "/Auth/Login";
                options.AccessDeniedPath = "/Auth/AccessDenied";
            });
        }

        /// <summary>
        /// This function set all the services URLs to the Static Details class (SD).
        /// </summary>
        /// <param name="services">Application service collection.</param>
        /// <param name="configuration">Application configuration.</param>
        public static void SeedServicesUrls(this IServiceCollection services, IConfiguration configuration)
        {
            SD.CouponAPIBase = configuration["ServiceUrls:CouponAPI"]!;
            SD.AuthAPIBase = configuration["ServiceUrls:AuthAPI"]!;
            SD.ProductAPIBase = configuration["ServiceUrls:ProductAPI"]!;
        }
    }
}
