using AutoMapper;
using Mango.Services.ShoppingCart.Web.Api.Models;
using Mango.Services.ShoppingCart.Web.Api.Models.Dto;

namespace Mango.Services.ShoppingCart.Web.Api
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(options =>
            {
                options.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                options.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
