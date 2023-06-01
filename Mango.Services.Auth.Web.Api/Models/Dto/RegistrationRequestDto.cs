namespace Mango.Services.Coupon.Web.Api.Models.Dto
{
    /// <summary>
    /// This is the user object that will be used to register information in "AspNetUsers" table.
    /// </summary>
    public class RegistrationRequestDto
    {
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

        /// <summary>
        /// Get and set password.
        /// </summary>
        public string Password { get; set; }
    }
}
