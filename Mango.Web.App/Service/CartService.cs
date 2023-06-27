using Mango.Web.App.Models;
using Mango.Web.App.Service.IService;
using Mango.Web.App.Utility;

namespace Mango.Web.App.Service
{
    /// <summary>
    /// This class implements the functions to use for shopping cart service.
    /// </summary>
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;

        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Function to get the shopping cart of an user.
        /// </summary>
        /// <param name="userId">User's unique identifier.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartAPIBase + "/api/cart/GetCart/" + userId
            });
        }

        /// <summary>
        /// Function to add or update a shooping cart for a user..
        /// </summary>
        /// <param name="cartDto">Cart object.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/CartUpsert"
            });
        }

        /// <summary>
        /// Function to remove a cart details from a shopping cart.
        /// </summary>
        /// <param name="cartDetailsId">Cart detail's unique identifier.</param>
        /// <returns></returns>
        public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDetailsId,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart"
            });
        }

        /// <summary>
        /// Function to add a coupon to shooping cart.
        /// </summary>
        /// <param name="cartDto">Cart object.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon"
            });
        }

        /// <summary>
        /// Function to remove a coupon to shooping cart.
        /// </summary>
        /// <param name="cartDto">Cart object.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> RemoveCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCoupon"
            });
        }

        /// <summary>
        /// Function to call a Azure Service Bus to "send" email.
        /// <para>
        /// For this project we only save the data in a database, we don't sent an email.
        /// </para>
        /// </summary>
        /// <param name="cartDto">Cart object.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> EmailCart(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/EmailCartRequest"
            });
        }
    }
}
