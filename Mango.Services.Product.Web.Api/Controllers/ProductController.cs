using AutoMapper;
using Mango.Services.Product.Web.Api.Data;
using Mango.Services.Product.Web.Api.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.Product.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Just signin users can use this service.
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public ProductController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                // Get list of Products from DB.
                IEnumerable<Models.Product> objList = _db.Products.ToList();
                // Use automapper to convert Product model to ProductDto.
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get([FromRoute] int id)
        {
            try
            {
                // Get the first elemento of the Product table with specific id.
                Models.Product obj = _db.Products.First(x => x.ProductId == id);
                // Use automapper to convert Product model to ProductDto.
                _response.Result = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")] // Only role admin can execute this method.
        public ResponseDto Post([FromBody] ProductDto ProductDto)
        {
            try
            {
                // Convert ProductDto to Product model and insert it to db.
                var Product = _mapper.Map<Models.Product>(ProductDto);
                _db.Products.Add(Product);
                _db.SaveChanges();
                // Use automapper to convert Product model to ProductDto.
                _response.Result = ProductDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] ProductDto ProductDto)
        {
            try
            {
                // Convert ProductDto to Product model and insert it to db.
                var Product = _mapper.Map<Models.Product>(ProductDto);
                _db.Products.Update(Product);
                _db.SaveChanges();
                // Use automapper to convert Product model to ProductDto.
                _response.Result = ProductDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete([FromRoute] int id)
        {
            try
            {
                // Get the first elemento of the Product table with specific id.
                Models.Product obj = _db.Products.First(x => x.ProductId == id);
                // Remove Product and save changes.
                _db.Products.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}
