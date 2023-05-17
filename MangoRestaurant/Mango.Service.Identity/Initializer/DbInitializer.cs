using IdentityModel;
using Mango.Service.Identity.DbContexts;
using Mango.Service.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Mango.Service.Identity.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            // Validamos si existe el rol "Admin", en caso de que no, creamos los roles de Admin y Customer.
            if (_roleManager.FindByNameAsync(SD.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
            }
            else
            {
                return;
            }
            // Si no existian los roles, vamos a agregar un nuevo usuario administrador.
            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "admin1@gmail.com",
                Email = "admin1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1111111",
                FirstName = "Ben",
                LastName = "Admin"
            };

            // Agregamos al administrador de usuarios el nuevo usuario con su contraseña "Admin123*".
            _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
            // Le establecemos el rol de Admin al usuario recién generado.
            _userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();
            // Agregamos un par de Claims al registro del usuario.
            var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[] {
                new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                new Claim(JwtClaimTypes.Role, SD.Admin),
            }).Result;

            // Si no existian los roles, vamos a agregar un nuevo usuario customer.
            ApplicationUser customerUser = new ApplicationUser()
            {
                UserName = "customer1@gmail.com",
                Email = "customer1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1111111",
                FirstName = "Ben",
                LastName = "Customer"
            };

            // Agregamos al nuevo usuario su contraseña.
            _userManager.CreateAsync(customerUser, "Admin123*").GetAwaiter().GetResult();
            // Le establecemos el rol de Customer al usuario recién generado.
            _userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();
            // Agregamos un par de Claims al registro del usuario.
            var temp2 = _userManager.AddClaimsAsync(customerUser, new Claim[] {
                new Claim(JwtClaimTypes.Name, customerUser.FirstName + " " + customerUser.LastName),
                new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
                new Claim(JwtClaimTypes.Role, SD.Customer),
            }).Result;
        }
    }
}
