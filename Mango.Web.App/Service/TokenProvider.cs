using Mango.Web.App.Service.IService;
using Mango.Web.App.Utility;
using Newtonsoft.Json.Linq;

namespace Mango.Web.App.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Set a new access token.
        /// </summary>
        /// <param name="token">Access token.</param>
        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
        }

        /// <summary>
        /// Get access token value.
        /// </summary>
        /// <returns>Access token.</returns>
        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);
            return hasToken is true ? token : null;
        }

        /// <summary>
        /// Clear the current access token.
        /// </summary>
        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
        }
    }
}
