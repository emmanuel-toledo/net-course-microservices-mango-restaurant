using Mango.Web.App.Models;
using Mango.Web.App.Service.IService;
using Mango.Web.App.Utility;

namespace Mango.Web.App.Service
{
    /// <summary>
    /// This class implements the "ICouponService" interface methods to consume Coupon REST Services.
    /// </summary>
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Get a coupon by unique coupone code.
        /// </summary>
        /// <param name="couponCode">Coupon unique code.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> GetCouponAsync(string couponCode) 
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/GetByCode/" + couponCode
            });
        }

        /// <summary>
        /// Get all the coupons.
        /// </summary>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> GetAllCoupons() 
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon"
            });
        }

        /// <summary>
        /// Get a coupon using a unique identifier.
        /// </summary>
        /// <param name="id">Coupon unique identifier.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> GetCouponByIdAsync(int id) 
        {
            return await _baseService.SendAsync(new RequestDto() 
            { 
                ApiType = SD.ApiType.GET, 
                Url = SD.CouponAPIBase + "/api/coupon" + id 
            });
        }

        /// <summary>
        /// Function to create a new Coupon record.
        /// </summary>
        /// <param name="coupondto">Coupon dto model.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> CreateCouponAsync(CouponDto coupondto) 
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.CouponAPIBase + "/api/coupon",
                Data = coupondto
            });
        }

        /// <summary>
        /// Function to update a Coupon record.
        /// </summary>
        /// <param name="coupondto">Coupon dto model.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto coupondto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.CouponAPIBase + "/api/coupon",
                Data = coupondto
            });
        }

        /// <summary>
        /// Function to delete Coupon record.
        /// </summary>
        /// <param name="coupon">Coupon dto model.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> DeleteCouponAsync(int id) 
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponAPIBase + "/api/coupon" + id
            });
        }
    }
}
