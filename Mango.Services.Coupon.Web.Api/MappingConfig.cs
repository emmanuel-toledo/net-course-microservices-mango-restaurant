using AutoMapper;
using Mango.Services.Coupon.Web.Api.Models.Dto;

namespace Mango.Services.Coupon.Web.Api
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(options =>
            {
                options.CreateMap<CouponDto, Models.Coupon>();
                options.CreateMap<Models.Coupon, CouponDto>();
            });
            return mappingConfig;
        }
    }
}
