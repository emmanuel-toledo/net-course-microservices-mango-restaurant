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

        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
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
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            // Retrive the user from the database.
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            // Validate if the password is correct.
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            // Validate if values are invalid.
            if(user is null || !isValid)
                return new LoginResponseDto() { User = null, Token = "" };

            // Generate Token.
            var token = _jwtTokenGenerator.GenerateToken(user);
            
            // Return the success value.
            UserDto userDto = new UserDto()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;
        }

        /// <summary>
        /// Function to add a new role to a specific user.
        /// </summary>
        /// <param name="email">Unique email address.</param>
        /// <param name="roleName">Role name to assign.</param>
        /// <returns>Flag is the assignment was correct.</returns>
        public async Task<bool> AssignRole(string email, string roleName)
        {
            // Retrive the user from the database.
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if(user != null)
            {
                // Validate if the role exists.
                if(!await _roleManager.RoleExistsAsync(roleName))
                {
                    // Create rol if it does not exist.
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }
    }
}
