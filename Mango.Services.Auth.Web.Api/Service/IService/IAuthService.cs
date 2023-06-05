using Mango.Services.Auth.Web.Api.Models.Dto;
using Mango.Services.Coupon.Web.Api.Models.Dto;

namespace Mango.Services.Auth.Web.Api.Service.IService
{
    /// <summary>
    /// This interface define the service methods that will be used for this api.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Function to register a new user.
        /// </summary>
        /// <param name="registrationRequestDto">Registration request model.</param>
        /// <returns>Error message the registration was not success.</returns>
        Task<string> Register(RegistrationRequestDto registrationRequestDto);

        /// <summary>
        /// Function to login a user in the application and generate a new JWT token.
        /// </summary>
        /// <param name="loginRequestDto">Login request model.</param>
        /// <returns>Login response model.</returns>
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        /// <summary>
        /// Function to add a new role to a specific user.
        /// </summary>
        /// <param name="email">Unique email address.</param>
        /// <param name="roleName">Role name to assign.</param>
        /// <returns>Flag is the assignment was correct.</returns>
        Task<bool> AssignRole(string email, string roleName);
    }
}
