namespace Mango.Services.ShoppingCart.Web.Api.Models.Dto
{
    /// <summary>
    /// This is the user object that will be used to return and receive information of "Coupon" table.
    /// </summary>
    public class CouponDto
    {
        /// <summary>
        /// Get and set unique identifier.
        /// </summary>
        public int CouponId { get; set; }

        /// <summary>
        /// Get and set coupon code.
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        /// Get and set discount amount.
        /// </summary>
        public double DiscountAmount { get; set; }

        /// <summary>
        /// Get and set minimum amount to apply this coupon.
        /// </summary>
        public int MinAmount { get; set; }
    }
}
