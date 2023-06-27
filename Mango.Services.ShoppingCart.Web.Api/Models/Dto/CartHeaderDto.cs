namespace Mango.Services.ShoppingCart.Web.Api.Models.Dto
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

        /// <summary>
        /// (Optional) Get and set user first name.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// (Optional) Get and set user last name.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// (Optional) Get and set user phone.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// (Optional) Get and set user email.
        /// </summary>
        public string? Email { get; set; }
    }
}
