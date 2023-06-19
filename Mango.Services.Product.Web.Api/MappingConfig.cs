using AutoMapper;
using Mango.Services.Product.Web.Api.Models.Dto;

namespace Mango.Services.Product.Web.Api
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(options =>
            {
                options.CreateMap<ProductDto, Models.Product>();
                options.CreateMap<Models.Product, ProductDto>();
            });
            return mappingConfig;
        }
    }
}
