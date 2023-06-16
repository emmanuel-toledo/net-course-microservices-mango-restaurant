using System.ComponentModel.DataAnnotations;

namespace Mango.Web.App.Models
{
    /// <summary>
    /// This is the user object that will be used to login a user with record in "AspNetUsers" table.
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// Get and set username.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Get and set password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
