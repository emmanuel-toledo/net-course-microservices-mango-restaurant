using AutoMapper;
using Mango.Services.Product.Web.Api.Data;
using Mango.Services.Product.Web.Api.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.Product.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [Authorize(Roles = "ADMIN")] // Only signin users with admin role can execute this method.
		public ResponseDto Post(ProductDto productDto)
        {
            try
            {
                // Convert ProductDto to Product model and insert it to db.
                var product = _mapper.Map<Models.Product>(productDto);
                _db.Products.Add(product);
                _db.SaveChanges();

                // We work with the image after to save to change the image's name with the product id.
                if(productDto.Image != null)
                {
                    // Set product image name with the product unique identifier and the image extension.
                    string fileName = product.ProductId + Path.GetExtension(productDto.Image.FileName);
                    string filePath = @"wwwroot\ProductImages\" + fileName;
                    // Complement the image path.
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using(var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        // Copy the image from productdto to our "wwwroot" folder in the project.
                        productDto.Image.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                    product.ImageLocalPath = filePath;
                } 
                else
                {
                    // If the image is null we set a default empty image.
                    product.ImageUrl = "https://placehold.co/600*400";
                }

                // Update image for product creation.
                _db.Products.Update(product);
                _db.SaveChanges();

                // Use automapper to convert Product model to ProductDto.
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")] // Only signin users with admin role can execute this method.
		public ResponseDto Put(ProductDto productDto) // We dont use [FromBody] because we send an image content.
        {
            try
            {
                // Convert ProductDto to Product model and insert it to db.
                var product = _mapper.Map<Models.Product>(productDto);

				// If an image was selected, we remove the one that exists.
				if (productDto.Image != null)
				{
                    // Delete image if that exists.
					if (!string.IsNullOrEmpty(product.ImageLocalPath))
					{
						// Obtenemos la ruta del archivo dentro de la web api.
						var olfFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
						FileInfo file = new FileInfo(olfFilePathDirectory);
						// Si el archivo existe lo eliminamos.
						if (file.Exists)
						{
							file.Delete();
						}
					}

					// Set product image name with the product unique identifier and the image extension.
					string fileName = product.ProductId + Path.GetExtension(productDto.Image.FileName);
					string filePath = @"wwwroot\ProductImages\" + fileName;
					// Complement the image path.
					var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
					using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
					{
						// Copy the image from productdto to our "wwwroot" folder in the project.
						productDto.Image.CopyTo(fileStream);
					}
					var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
					product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
					product.ImageLocalPath = filePath;
				}

				_db.Products.Update(product);
                _db.SaveChanges();
                // Use automapper to convert Product model to ProductDto.
                _response.Result = productDto;
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
        [Authorize(Roles = "ADMIN")] // Only signin users with admin role can execute this method.
		public ResponseDto Delete([FromRoute] int id)
        {
            try
            {
                // Get the first elemento of the Product table with specific id.
                Models.Product obj = _db.Products.First(x => x.ProductId == id);

                if (!string.IsNullOrEmpty(obj.ImageLocalPath))
                {
                    // Obtenemos la ruta del archivo dentro de la web api.
                    var olfFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), obj.ImageLocalPath);
                    FileInfo file = new FileInfo(olfFilePathDirectory);
                    // Si el archivo existe lo eliminamos.
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }

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
