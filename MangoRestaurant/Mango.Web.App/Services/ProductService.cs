﻿using Mango.Web.App.Models;
using Mango.Web.App.Services.IServices;

namespace Mango.Web.App.Services
{
    public class ProductService : BaseService, IProductService
    {
        // Propiedad que será poblada con la inyección de dependencias.
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateProductAsync<T>(ProductsDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                RequestType = SD.RequestType.POST,
                Data = productDto,
                URL = SD.ProductAPI + "/api/products",
                AccessToken = string.Empty
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                RequestType = SD.RequestType.DELETE,
                URL = SD.ProductAPI + "/api/products/" + id,
                AccessToken = string.Empty
            });
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                RequestType = SD.RequestType.GET,
                URL = SD.ProductAPI + "/api/products",
                AccessToken = string.Empty
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                RequestType = SD.RequestType.GET,
                URL = SD.ProductAPI + "/api/products/" + id,
                AccessToken = string.Empty
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductsDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                RequestType = SD.RequestType.PUT,
                Data = productDto,
                URL = SD.ProductAPI + "/api/products",
                AccessToken = string.Empty
            });
        }
    }
}
