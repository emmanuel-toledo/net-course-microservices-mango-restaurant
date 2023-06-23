using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCart.Web.Api.Models
{
    /// <summary>
    /// This class defines a cart header in the database.
    /// </summary>
    public class CartHeader
    {
        /// <summary>
        /// Get and set cart header unique identifier.
        /// </summary>
        [Key]
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
        [NotMapped]
        public double Discount { get; set; }

        /// <summary>
        /// Get and set cart header total.
        /// </summary>
        [NotMapped]
        public double CartTotal { get; set; }
    }
}
