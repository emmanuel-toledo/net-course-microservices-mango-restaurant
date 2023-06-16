namespace Mango.Web.App.Models
{
    /// <summary>
    /// This is the user object that will be used to return information of "AspNetUsers" table.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Get and set unique identifier.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Get and set unique email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Get and set name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get and set phone number.
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
