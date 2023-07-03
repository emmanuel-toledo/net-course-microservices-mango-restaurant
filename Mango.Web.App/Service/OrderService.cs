using Mango.Web.App.Models;
using Mango.Web.App.Service.IService;
using Mango.Web.App.Utility;

namespace Mango.Web.App.Service
{
    /// <summary>
    /// This class implements the functions to use for Order Service.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;

        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Function to create a new Order in the database.
        /// </summary>
        /// <param name="cartDto">Cart model.</param>
        /// <param name="cartDto">Cart to model.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> CreateOrderAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.OrderAPIBase + "/api/order/CreateOrder",
                Data = cartDto
            });
        }

        /// <summary>
        /// Function to create a new session for stripe payment integration.
        /// </summary>
        /// <param name="stripeRequestDto">Stripe request for stripe library.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> CreateStripeSession(StripeRequestDto stripeRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.OrderAPIBase + "/api/order/CreateStripeSession",
                Data = stripeRequestDto
            });
        }

        /// <summary>
        /// Function to validate the status of a payment in stripe.
        /// </summary>
        /// <param name="orderHeaderId">Order header id</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> ValidateStripeSession(int orderHeaderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.OrderAPIBase + "/api/order/ValidateStripeSession",
                Data = orderHeaderId
            });
        }
    }
}
