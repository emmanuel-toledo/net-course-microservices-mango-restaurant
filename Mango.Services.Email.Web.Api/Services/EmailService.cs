using Mango.Services.Email.Web.Api.Data;
using Mango.Services.Email.Web.Api.Message;
using Mango.Services.Email.Web.Api.Models;
using Mango.Services.Email.Web.Api.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Mango.Services.Email.Web.Api.Services
{
    /// <summary>
    /// This class implements all the functions to send an email (Save it in the database).
    /// </summary>
    public class EmailService : IEmailService
    {
        private DbContextOptions<AppDbContext> _dbOptions;

        public EmailService(DbContextOptions<AppDbContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        /// <summary>
        /// Function to create email template and save it inside the database when the user select "Email sent" button in the web application for shopping cart.
        /// </summary>
        /// <param name="cartDto">Shopping cart information.</param>
        /// <returns>Async Task.</returns>
        public async Task EmailCartAndLog(CartDto cartDto)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine("<br /> Cart Email Requested");
            message.AppendLine("<br /> Total " + cartDto.CartHeader.CartTotal);
            message.Append("<br />");
            message.Append("<ul>");

            foreach(var item in cartDto.CartDetails)
            {
                message.Append("<li>");
                message.Append(item.Product.Name + " x " + item.Count);
                message.Append("</li>");
            }

            message.Append("</ul>");

            await LogAndEmail(message.ToString(), cartDto.CartHeader.Email);
        }

        /// <summary>
        /// Function to create email template and save it inside the database for the creation of a new user.
        /// </summary>
        /// <param name="email">New user email.</param>
        /// <returns>Async Task.</returns>
        public async Task RegisterUserEmailAndLog(string email)
        {
            string message = "User registration successful <bt /> Email: " + email;
            await LogAndEmail(message, "admin@contoso.com");
        }

        /// <summary>
        /// Function to log a reward message for "OrderCreatedEmail" in "OrderCreated" topic in Azure Service Bus.
        /// </summary>
        /// <param name="rewardsMessage">Reward message.</param>
        /// <returns>Async Task.</returns>
        public async Task LogOrderPlaced(RewardsMessage rewardsMessage)
        {
            string message = "New Order Placed. <bt /> Order ID: " + rewardsMessage.OrderId;
            await LogAndEmail(message, "admin@contoso.com");
        }

        /// <summary>
        /// Function to save an email inside the database.
        /// <para>
        /// This function can be updated to call a service to sent the email address.
        /// </para>
        /// </summary>
        /// <param name="message">Message of email.</param>
        /// <param name="email">Email address.</param>
        /// <returns>True as success false as failed.</returns>
        private async Task<bool> LogAndEmail(string message, string email)
        {
            try
            {
                EmailLogger emailLog = new()
                {
                    Email = email,
                    EmailSent = DateTime.Now,
                    Message = message
                };

                // Create a new instance of AppDbContext to access to the database using EF Core.
                await using var _db = new AppDbContext(_dbOptions);

                await _db.EmailLoggers.AddAsync(emailLog);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
