using Mango.Product.Web.Api.Models.Dto;

namespace Mango.Product.Web.Api.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductsDto>> GetProducts();

        Task<ProductsDto> GetProductById(int productId);

        Task<ProductsDto> CreateUpdateProduct(ProductsDto productDto);

        Task<bool> DeleteProduct(int productId);
    }
}
