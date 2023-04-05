using AutoMapper;
using Mango.Product.Web.Api.DbContexts;
using Mango.Product.Web.Api.Models;
using Mango.Product.Web.Api.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Product.Web.Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        // Aplicamos la Injección de dependencias para la conexión a la base de datos y el mapeo de los modelos.
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        // Aplicamos la Injección de dependencias para la conexión a la base de datos y el mapeo de los modelos.
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductsDto>> GetProducts()
        {
            List<Products> products = await _db.Products.ToListAsync(); // Obtenemos la lista de productos.
            return _mapper.Map<List<ProductsDto>>(products);
        }

        public async Task<ProductsDto> GetProductById(int productId)
        {
            Products products = await _db.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync(); // Obtenemos producto por id.
            return _mapper.Map<ProductsDto>(products);
        }

        public async Task<ProductsDto> CreateUpdateProduct(ProductsDto productDto)
        {
            Products product = _mapper.Map<ProductsDto, Products>(productDto);
            if(product.ProductId > 0)
            {
                _db.Products.Update(product);
            }
            else
            {
                _db.Products.Add(product);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Products, ProductsDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Products product = await _db.Products.FirstOrDefaultAsync(x => x.ProductId == productId); // Obtenemos producto por id.
                if(product == null)
                {
                    return false;
                }
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            } catch(Exception ex)
            {
                return false;
            }
        }
    }
}
