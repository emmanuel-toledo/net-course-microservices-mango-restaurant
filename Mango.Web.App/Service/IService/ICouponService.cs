using Mango.Web.App.Models;

namespace Mango.Web.App.Service.IService
{
    /// <summary>
    /// This interface defines the functions to use for Coupon Service.
    /// </summary>
    public interface ICouponService
    {
        /// <summary>
        /// Get a coupon by unique coupone code.
        /// </summary>
        /// <param name="couponCode">Coupon unique code.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> GetCouponAsync(string couponCode);

        /// <summary>
        /// Get all the coupons.
        /// </summary>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> GetAllCoupons();

        /// <summary>
        /// Get a coupon using a unique identifier.
        /// </summary>
        /// <param name="id">Coupon unique identifier.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> GetCouponByIdAsync(int id);

        /// <summary>
        /// Function to create a new Coupon record.
        /// </summary>
        /// <param name="coupon">Coupon dto model.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> CreateCouponAsync(CouponDto coupondto);

        /// <summary>
        /// Function to update a Coupon record.
        /// </summary>
        /// <param name="coupon">Coupon dto model.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> UpdateCouponAsync(CouponDto coupondto);

        /// <summary>
        /// Function to delete Coupon record.
        /// </summary>
        /// <param name="coupon">Coupon dto model.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> DeleteCouponAsync(int id);
    }
}
