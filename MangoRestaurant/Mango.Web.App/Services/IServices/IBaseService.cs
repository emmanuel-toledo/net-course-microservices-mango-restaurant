using Mango.Web.App.Models;

namespace Mango.Web.App.Services.IServices
{
    public interface IBaseService : IDisposable
    {
        ResponseDto response { get; set; }

        Task<T> SendAsync<T>(ApiRequest request);
    }
}
