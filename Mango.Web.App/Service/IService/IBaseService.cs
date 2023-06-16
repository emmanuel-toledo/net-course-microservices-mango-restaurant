using Mango.Web.App.Models;

namespace Mango.Web.App.Service.IService
{
    /// <summary>
    /// Base interface to consume REST Services from Mango web app.
    /// </summary>
    public interface IBaseService
    {
        /// <summary>
        /// Method to execute a new request to a microservice.
        /// </summary>
        /// <param name="requestDto">Request configuration model.</param>
        /// <param name="withBearer">Flag to validate if the request requires access token.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
