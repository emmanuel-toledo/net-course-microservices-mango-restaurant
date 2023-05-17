using Mango.Product.Web.Api.Models.Dto;
using Mango.Product.Web.Api.Repository;
using Mango.Service.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Product.Web.Api.Controllers
{
    [Route("api/products")] // Agregamos ruta de nuestro controlador.
    public class ProductController : ControllerBase // Usamos ControllerBase ya que es el recomendado para APIs.
    {
        protected ResponseDto _response; // Generamos propiedad ResponseDto ya que nuestras respuestas no serán genericas.

        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
            this._response = new ResponseDto(); // Inicializamos nuestra respuesta como una respuesta vacia.
        }

        [Authorize]
        [HttpGet] // Definimos el tipo de petición que tendrá nuestro servicio.
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductsDto> productsDto = await _repository.GetProducts(); // Ejecutamos función de nuestro repository.
                _response.Result = productsDto; // Establecemos el resultado en nuestra variable _response.
            } catch (Exception ex) // En caso de error establecemos valores como negativos y mensajes de error.
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response; // Regresamos como respuesta nuestro objeto Response.
        }

        [Authorize]
        [HttpGet] // Definimos el tipo de petición que tendrá nuestro servicio.
        [Route(("{id}"))] // Definimos la ruta de nuestro método con un parámetro llamado "id".
        public async Task<object> Get(int id)
        {
            try
            {
                ProductsDto productDto = await _repository.GetProductById(id); // Ejecutamos función de nuestro repository.
                _response.Result = productDto; // Establecemos el resultado en nuestra variable _response.
            }
            catch (Exception ex) // En caso de error establecemos valores como negativos y mensajes de error.
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response; // Regresamos como respuesta nuestro objeto Response.
        }

        [HttpPost] // Definimos el tipo de petición que tendrá nuestro servicio.
        public async Task<object> Post([FromBody] ProductsDto productDto)
        {
            try
            {
                ProductsDto model = await _repository.CreateUpdateProduct(productDto); // Ejecutamos función de nuestro repository.
                _response.Result = model; // Establecemos el resultado en nuestra variable _response.
            }
            catch (Exception ex) // En caso de error establecemos valores como negativos y mensajes de error.
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response; // Regresamos como respuesta nuestro objeto Response.
        }

        [Authorize]
        [HttpPut] // Definimos el tipo de petición que tendrá nuestro servicio.
        public async Task<object> Put([FromBody] ProductsDto productDto)
        {
            try
            {
                ProductsDto model = await _repository.CreateUpdateProduct(productDto); // Ejecutamos función de nuestro repository.
                _response.Result = model; // Establecemos el resultado en nuestra variable _response.
            }
            catch (Exception ex) // En caso de error establecemos valores como negativos y mensajes de error.
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response; // Regresamos como respuesta nuestro objeto Response.
        }

        [Authorize(Roles = SD.Admin)]
        [HttpDelete] // Definimos el tipo de petición que tendrá nuestro servicio.
        [Route(("{id}"))] // Definimos la ruta de nuestro método con un parámetro llamado "id".
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _repository.DeleteProduct(id); // Ejecutamos función de nuestro repository.
                _response.Result = isSuccess; // Establecemos el resultado en nuestra variable _response.
            }
            catch (Exception ex) // En caso de error establecemos valores como negativos y mensajes de error.
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response; // Regresamos como respuesta nuestro objeto Response.
        }
    }
}
