using AutoMapper;
using Mango.Product.Web.Api.Models;
using Mango.Product.Web.Api.Models.Dto;

namespace Mango.Product.Web.Api.AutoMapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            // Generamos mapeo de las clases Products y ProductsDto.
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Products, ProductsDto>();
                config.CreateMap<ProductsDto, Products>();
            });
            return mappingConfig;
        }
    }
}
