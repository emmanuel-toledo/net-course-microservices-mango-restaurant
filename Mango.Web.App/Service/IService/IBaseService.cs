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
        /// <returns>Response model.</returns>
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
