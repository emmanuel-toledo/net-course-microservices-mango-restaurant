using Microsoft.AspNetCore.Identity;

namespace Mango.Services.Auth.Web.Api.Models
{
    /// <summary>
    /// This class help us to define new properties for our Identity User and modify the tables properties for in our database.
    /// <para>
    /// Requires to extends "IdentityUser" class.
    /// </para>
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Get and set the User name.
        /// </summary>
        public string Name { get; set; }
    }
}
