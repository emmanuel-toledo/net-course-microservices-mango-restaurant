using Mango.Services.Auth.Web.Api.Models;

namespace Mango.Services.Auth.Web.Api.Service.IService
{
    /// <summary>
    /// This interface defines the functions to create a new token according to a success login.
    /// </summary>
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Generate a new access token.
        /// </summary>
        /// <param name="applicationUser">Succes user login.</param>
        /// <returns>Token string.</returns>
        string GenerateToken(ApplicationUser applicationUser);
    }
}
