using System.ComponentModel.DataAnnotations;

namespace Mango.Services.Product.Web.Api.Models
{
    /// <summary>
    /// This class contains the deffinition of a product in the DB.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Get and set the unique identifier of a product.
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// Get and set the name of a product.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Get and set the price of a product.
        /// </summary>
        [Range(0, 1000)]
        public double Price { get; set; }

        /// <summary>
        /// Get and set the description of a product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get and set the category name of a product.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Get and set the image url of a product.
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
