using Mango.Web.App.Models;
using Mango.Web.App.Service.IService;
using Mango.Web.App.Utility;

namespace Mango.Web.App.Service
{
    /// <summary>
    /// This class implements the functions to use for Product Service.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Get all the Products.
        /// </summary>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product"
            });
        }

        /// <summary>
        /// Get a Product using a unique identifier.
        /// </summary>
        /// <param name="id">Product unique identifier.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product/" + id
            });
        }

        /// <summary>
        /// Function to create a new Product record.
        /// </summary>
        /// <param name="model">Product dto model.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> CreateProductAsync(ProductDto model)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ProductAPIBase + "/api/product",
                Data = model,
                ContentType = SD.ContentType.MultipartFormData
            });
        }

        /// <summary>
        /// Function to update a Product record.
        /// </summary>
        /// <param name="model">Product dto model.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> UpdateProductAsync(ProductDto model)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.ProductAPIBase + "/api/product",
                Data = model,
				ContentType = SD.ContentType.MultipartFormData
			});
        }

        /// <summary>
        /// Function to delete Product record.
        /// </summary>
        /// <param name="Product">Product dto model.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> DeleteProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto() 
            { 
                ApiType = SD.ApiType.DELETE, 
                Url = SD.ProductAPIBase + "/api/product/" + id 
            });
        }
    }
}
