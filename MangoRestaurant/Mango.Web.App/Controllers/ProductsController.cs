using Mango.Web.App.Models;
using Mango.Web.App.Services.IServices;
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
            var response = await _productService.GetAllProductsAsync<ResponseDto>();
            if(response.Result != null)
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
                var response = await _productService.CreateProductAsync<ResponseDto>(model);
                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        public async Task<ActionResult> ProductEdit(int productId) 
        {
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
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
                var response = await _productService.UpdateProductAsync<ResponseDto>(model);
                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        public async Task<ActionResult> ProductDelete(int productId)
        {
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                ProductsDto product = JsonConvert.DeserializeObject<ProductsDto>(Convert.ToString(response.Result)!)!;
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProductDelete(ProductsDto model)
        {
            var response = await _productService.DeleteProductAsync<ResponseDto>(model.ProductId);
            if (response != null)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }
    }
}
