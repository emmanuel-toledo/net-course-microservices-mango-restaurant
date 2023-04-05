using System.ComponentModel.DataAnnotations;

namespace Mango.Product.Web.Api.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1, 1000)]
        public double Price { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string? Description { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string? CategoryName { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string? ImageUrl { get; set; }
    }
}
