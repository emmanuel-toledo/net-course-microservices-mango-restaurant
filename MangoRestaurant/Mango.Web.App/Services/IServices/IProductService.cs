using Mango.Web.App.Models;

namespace Mango.Web.App.Services.IServices
{
    public interface IProductService : IBaseService
    {
        Task<T> GetAllProductsAsync<T>();

        Task<T> GetProductByIdAsync<T>(int id);

        Task<T> CreateProductAsync<T>(ProductsDto productDto);

        Task<T> UpdateProductAsync<T>(ProductsDto productDto);

        Task<T> DeleteProductAsync<T>(int id);
    }
}
