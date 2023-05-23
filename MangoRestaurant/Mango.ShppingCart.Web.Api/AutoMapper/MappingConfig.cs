using AutoMapper;

namespace Mango.ShoppingCart.Web.Api.AutoMapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            // Generamos mapeo de las clases Products y ProductsDto.
            var mappingConfig = new MapperConfiguration(config =>
            {
            });
            return mappingConfig;
        }
    }
}
