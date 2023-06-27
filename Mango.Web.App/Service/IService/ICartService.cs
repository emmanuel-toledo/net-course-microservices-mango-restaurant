using Mango.Web.App.Models;

namespace Mango.Web.App.Service.IService
{
    /// <summary>
    /// This interface defines the functions to use for shopping cart service.
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Function to get the shopping cart of an user.
        /// </summary>
        /// <param name="userId">User's unique identifier.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> GetCartByUserIdAsync(string userId);

        /// <summary>
        /// Function to add or update a shooping cart for a user.
        /// </summary>
        /// <param name="cartDto">Cart object.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);

        /// <summary>
        /// Function to remove a cart details from a shopping cart.
        /// </summary>
        /// <param name="cartDetailsId">Cart detail's unique identifier.</param>
        /// <returns></returns>
        Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId);

        /// <summary>
        /// Function to add a coupon to shooping cart.
        /// </summary>
        /// <param name="cartDto">Cart object.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);

        /// <summary>
        /// Function to remove a coupon to shooping cart.
        /// </summary>
        /// <param name="cartDto">Cart object.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> RemoveCouponAsync(CartDto cartDto);

        /// <summary>
        /// Function to call a Azure Service Bus to "send" email.
        /// <para>
        /// For this project we only save the data in a database, we don't sent an email.
        /// </para>
        /// </summary>
        /// <param name="cartDto">Cart object.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> EmailCart(CartDto cartDto);
    }
}
