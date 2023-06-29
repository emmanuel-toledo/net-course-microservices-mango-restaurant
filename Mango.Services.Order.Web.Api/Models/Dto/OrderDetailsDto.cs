namespace Mango.Services.Order.Web.Api.Models.Dto
{
    /// <summary>
    /// This class contains the definition of order detail model.
    /// </summary>
    public class OrderDetailsDto
    {
        /// <summary>
        /// Get and set order details unique identifier.
        /// </summary>
        public int OrderDetailsId { get; set; }

        /// <summary>
        /// Get and set order details related order header unique identifier.
        /// </summary>
        public int OrderHeaderId { get; set; }

        /// <summary>
        /// Get and set order details count of products.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Get and set order details related product unique identifier.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Get and set order details related product object.
        /// </summary>
        public ProductDto? Product { get; set; }

        /// <summary>
        /// Get and set the product name of the order.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Get and set the product price of the order.
        /// </summary>
        public string Price { get; set; }
    }
}
