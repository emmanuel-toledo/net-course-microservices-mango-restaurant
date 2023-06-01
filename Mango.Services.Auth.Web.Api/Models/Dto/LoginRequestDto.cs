namespace Mango.Services.Auth.Web.Api.Models.Dto
{
    /// <summary>
    /// This is the user object that will be used to login a user with record in "AspNetUsers" table.
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// Get and set username.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Get and set password.
        /// </summary>
        public string Password { get; set; }
    }
}
