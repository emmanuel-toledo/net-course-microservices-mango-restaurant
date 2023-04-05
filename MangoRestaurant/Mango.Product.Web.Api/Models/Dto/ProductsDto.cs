namespace Mango.Product.Web.Api.Models.Dto
{
    /// <summary>
    /// Modelo que se utilizará al momento de enviar solicitudes HTTP al servicio de Productos.
    /// </summary>
    public class ProductsDto
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string? Description { get; set; }

        public string? CategoryName { get; set; }

        public string? ImageUrl { get; set; }
    }
}
