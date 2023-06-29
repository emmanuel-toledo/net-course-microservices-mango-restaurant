namespace Mango.Web.App.Models
{
    /// <summary>
    /// This class contains the definition of an Oder Header for Mango Web Application.
    /// </summary>
    public class OrderHeaderDto
    {
        /// <summary>
        /// Get and set cart header unique identifier.
        /// </summary>
        public int OrderHeaderId { get; set; }

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
        public double OrderTotal { get; set; }

        /// <summary>
        /// Get and set user name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Get and set user phone.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Get and set user email.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Get and set the order time.
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// Get and set the order status.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Get and set the order payment intent id.
        /// </summary>
        public string? PaymentIntentId { get; set; }

        /// <summary>
        /// Get and set the order stripe session id.
        /// </summary>
        public string? StripeSessionId { get; set; }

        /// <summary>
        /// Get and set the collection of order details.
        /// </summary>
        public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }
    }    
}
