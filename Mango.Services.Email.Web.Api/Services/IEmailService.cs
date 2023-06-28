using Mango.Services.Email.Web.Api.Models.Dto;

namespace Mango.Services.Email.Web.Api.Services
{
    /// <summary>
    /// This interface defines all the functions to send an email (Save it in the database).
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Function to create email template and save it inside the database when the user select "Email sent" button in the web application for shopping cart.
        /// </summary>
        /// <param name="cartDto">Shopping cart information.</param>
        /// <returns>Async Task.</returns>
        Task EmailCartAndLog(CartDto cartDto);

        /// <summary>
        /// Function to create email template and save it inside the database for the creation of a new user.
        /// </summary>
        /// <param name="email">New user email.</param>
        /// <returns>Async Task.</returns>
        Task RegisterUserEmailAndLog(string email);
    }
}
