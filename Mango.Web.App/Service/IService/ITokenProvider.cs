namespace Mango.Web.App.Service.IService
{
    /// <summary>
    /// This interface defines the methods to interact with an access token.
    /// </summary>
    public interface ITokenProvider
    {
        /// <summary>
        /// Set a new access token.
        /// </summary>
        /// <param name="token">Access token.</param>
        void SetToken(string token);

        /// <summary>
        /// Get access token value.
        /// </summary>
        /// <returns>Access token.</returns>
        string? GetToken();

        /// <summary>
        /// Clear the current access token.
        /// </summary>
        void ClearToken();
    }
}
