using AutoMapper;
using Mango.Services.Order.Web.Api.Models;
using Mango.Services.Order.Web.Api.Models.Dto;

namespace Mango.Services.Order.Web.Api
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(options =>
            {
                options.CreateMap<OrderHeaderDto, CartHeaderDto>()
                    .ForMember(dest => dest.CartTotal, u => u.MapFrom(src => src.OrderTotal)).ReverseMap();

                options.CreateMap<CartDetailsDto, OrderDetailsDto>()
                    .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product.Name))
                    .ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product.Price));

                options.CreateMap<OrderHeaderDto, OrderHeader>().ReverseMap();
                options.CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
