using Mango.Web.App.Models;

namespace Mango.Web.App.Service.IService
{
    /// <summary>
    /// This interface defines the functions to use for Order Service.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Function to create a new Order in the database.
        /// </summary>
        /// <param name="cartDto">Cart model.</param>
        /// <param name="cartDto">Cart to model.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> CreateOrderAsync(CartDto cartDto);

        /// <summary>
        /// Function to create a new session for stripe payment integration.
        /// </summary>
        /// <param name="stripeRequestDto">Stripe request for stripe library.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> CreateStripeSession(StripeRequestDto stripeRequestDto);

        /// <summary>
        /// Function to validate the status of a payment in stripe.
        /// </summary>
        /// <param name="orderHeaderId">Order header id</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> ValidateStripeSession(int orderHeaderId);

        /// <summary>
        /// Function to retrive a list of all the orders in the database.
        /// </summary>
        /// <param name="userId">User's unique identifier.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> GetAllOrders(string? userId);

        /// <summary>
        /// Function to retrive an order by unique identifier.
        /// </summary>
        /// <param name="orderId">Order's unique identifier.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> GetOrder(int orderId);

        /// <summary>
        /// Function to update an order's status.
        /// </summary>
        /// <param name="orderId">Order's unique identifier.</param>
        /// <param name="newStatus">New status.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> UpdateOrderStatus(int orderId, string newStatus);
    }
}
