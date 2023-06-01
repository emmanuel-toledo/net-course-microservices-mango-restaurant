using Mango.Services.Auth.Web.Api.Data;
using Mango.Services.Auth.Web.Api.Models;
using Mango.Services.Auth.Web.Api.Models.Dto;
using Mango.Services.Auth.Web.Api.Service.IService;
using Mango.Services.Coupon.Web.Api.Models.Dto;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.Auth.Web.Api.Service
{
    /// <summary>
    /// This class implements the service methods that will be used for this api.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Function to register a new user.
        /// </summary>
        /// <param name="registrationRequestDto">Registration request model.</param>
        /// <returns>Error message the registration was not success.</returns>
        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            // Generate a new instance of application user.
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber,
            };
            // Insert new user inside de DB.
            try
            {
                // Insert new user using "_userManager". The password was used as a second param because this library automatically hash it.
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                // The registration was success.
                if (result.Succeeded)
                {
                    // Once user is register, we access to the database and get the new user.
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);
                    // Parse the user response model.
                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return "";
                } 
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Function to login a user in the application and generate a new JWT token.
        /// </summary>
        /// <param name="loginRequestDto">Login request model.</param>
        /// <returns>Login response model.</returns>
        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
