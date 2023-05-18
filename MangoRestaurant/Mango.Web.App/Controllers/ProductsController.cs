using Mango.Web.App.Models;
using Mango.Web.App.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.App.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ActionResult> ProductIndex()
        {
            List<ProductsDto> products = new List<ProductsDto>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetAllProductsAsync<ResponseDto>(accessToken);
            if(response != null && response.Result != null)
            {
                products = JsonConvert.DeserializeObject<List<ProductsDto>>(Convert.ToString(response.Result)!)!;
            }
            return View(products);
        }

        public ActionResult ProductCreate() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProductCreate(ProductsDto model)
        {
            if(ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.CreateProductAsync<ResponseDto>(model, accessToken);
                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        public async Task<ActionResult> ProductEdit(int productId) 
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if(response != null && response.IsSuccess)
            {
                ProductsDto product = JsonConvert.DeserializeObject<ProductsDto>(Convert.ToString(response.Result)!)!;
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProductEdit(ProductsDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.UpdateProductAsync<ResponseDto>(model, accessToken);
                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ProductDelete(int productId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if (response != null && response.IsSuccess)
            {
                ProductsDto product = JsonConvert.DeserializeObject<ProductsDto>(Convert.ToString(response.Result)!)!;
                return View(product);
            }
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProductDelete(ProductsDto model)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductAsync<ResponseDto>(model.ProductId, accessToken);
            if (response != null)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }
    }
}
