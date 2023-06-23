using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mango.Services.ShoppingCart.Web.Api.Models.Dto;

namespace Mango.Services.ShoppingCart.Web.Api.Models
{
    /// <summary>
    /// This class defines the cart details property for the database.
    /// </summary>
    public class CartDetails
    {
        /// <summary>
        /// Get and set cart details unique identifier.
        /// </summary>
        [Key]
        public int CartDetailsId { get; set; }

        /// <summary>
        /// Get and set cart details related cart header unique identifier.
        /// </summary>
        public int CartHeaderId { get; set; }

        /// <summary>
        /// Get and set cart details related cart header object.
        /// </summary>
        [ForeignKey(nameof(CartHeaderId))]
        public CartHeader CartHeader { get; set; }

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
        [NotMapped]
        public ProductDto Product { get; set; }
    }
}
