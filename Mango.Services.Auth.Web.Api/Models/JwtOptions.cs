namespace Mango.Services.Auth.Web.Api.Models
{
    /// <summary>
    /// This model contains the configuration options of an authentication token.
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Get and set the secret password.
        /// </summary>
        public string Secret { get; set; } = string.Empty;

        /// <summary>
        /// Get and set who is the one who provides the token.
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Get and set who is the one client that can request a token.
        /// </summary>
        public string Audience { get; set; } = string.Empty;
    }
}
