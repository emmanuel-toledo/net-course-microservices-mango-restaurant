namespace Mango.Web.App.Models
{
    /// <summary>
    /// This class defines the cart details property for the database.
    /// </summary>
    public class CartDetailsDto
    {
        /// <summary>
        /// Get and set cart details unique identifier.
        /// </summary>
        public int CartDetailsId { get; set; }

        /// <summary>
        /// Get and set cart details related cart header unique identifier.
        /// </summary>
        public int CartHeaderId { get; set; }

        /// <summary>
        /// Get and set cart details related cart header object.
        /// </summary>
        public CartHeaderDto? CartHeader { get; set; }

        /// <summary>
        /// Get and set cart details count of products.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Get and set cart details related product unique identifier.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Get and set cart details related product object.
        /// </summary>
        public ProductDto? Product { get; set; }
    }
}
