using Mango.Web.App.Models;

namespace Mango.Web.App.Services.IServices
{
    public interface IProductService : IBaseService
    {
        Task<T> GetAllProductsAsync<T>(string token);

        Task<T> GetProductByIdAsync<T>(int id, string token);

        Task<T> CreateProductAsync<T>(ProductsDto productDto, string token);

        Task<T> UpdateProductAsync<T>(ProductsDto productDto, string token);

        Task<T> DeleteProductAsync<T>(int id, string token);
    }
}
