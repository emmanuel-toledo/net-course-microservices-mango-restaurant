using Mango.Services.Order.Web.Api.Models.Dto;

namespace Mango.Services.Order.Web.Api.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
