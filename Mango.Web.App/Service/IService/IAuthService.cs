using Mango.Web.App.Models;

namespace Mango.Web.App.Service.IService
{
    /// <summary>
    /// This interface defines the functions to use for Auth Service.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Function to login an user into the application.
        /// </summary>
        /// <param name="loginRequestDto">Login request model.</param>
        /// <returns>Response model instance.</returns>
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);

        /// <summary>
        /// Function to create a new user inside the application.
        /// </summary>
        /// <param name="registrationRequestDto">Registration request model.</param>
        /// <returns>Response model instance.</returns>
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);

        /// <summary>
        /// Function to set a new rol to a user into the application.
        /// </summary>
        /// <param name="registrationRequestDto">Registration request model.</param>
        /// <returns>Response model instance.</returns>
        Task<ResponseDto?> AssingnRoleAsync(RegistrationRequestDto registrationRequestDto);
    }
}
