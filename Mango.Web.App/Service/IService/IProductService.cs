using Mango.Web.App.Models;

namespace Mango.Web.App.Service.IService
{
    /// <summary>
    /// This interface defines the functions to use for Product Service.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get all the Products.
        /// </summary>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> GetAllProductsAsync();

        /// <summary>
        /// Get a Product using a unique identifier.
        /// </summary>
        /// <param name="id">Product unique identifier.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> GetProductByIdAsync(int id);

        /// <summary>
        /// Function to create a new Product record.
        /// </summary>
        /// <param name="model">Product dto model.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> CreateProductAsync(ProductDto model);

        /// <summary>
        /// Function to update a Product record.
        /// </summary>
        /// <param name="model">Product dto model.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> UpdateProductAsync(ProductDto model);

        /// <summary>
        /// Function to delete Product record.
        /// </summary>
        /// <param name="Product">Product dto model.</param>
        /// <returns>Response model.</returns>
        Task<ResponseDto?> DeleteProductAsync(int id);
    }
}
