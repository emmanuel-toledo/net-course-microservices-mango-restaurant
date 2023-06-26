namespace Mango.Web.App.Models
{
    /// <summary>
    /// This class defines a cart header in the database.
    /// </summary>
    public class CartHeaderDto
    {
        /// <summary>
        /// Get and set cart header unique identifier.
        /// </summary>
        public int CartHeaderId { get; set; }

        /// <summary>
        /// Get and set cart header user unique identifier.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Get and set cart header coupon code.
        /// </summary>
        public string? Couponcode { get; set; }

        /// <summary>
        /// Get and set cart header discount amount.
        /// </summary>
        public double Discount { get; set; }

        /// <summary>
        /// Get and set cart header total.
        /// </summary>
        public double CartTotal { get; set; }
    }
}
