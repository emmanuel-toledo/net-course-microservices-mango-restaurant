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
    }
}
