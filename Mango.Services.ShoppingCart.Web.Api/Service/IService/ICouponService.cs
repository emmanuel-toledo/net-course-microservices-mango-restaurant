using Mango.Services.ShoppingCart.Web.Api.Models.Dto;

namespace Mango.Services.ShoppingCart.Web.Api.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
