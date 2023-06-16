namespace Mango.Web.App.Models
{
    /// <summary>
    /// This class works as the response of a successfully login.
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// Get and set the user information.
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// Get and set the access Token.
        /// </summary>
        public string Token { get; set; }
    }
}
