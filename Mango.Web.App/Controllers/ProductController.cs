using Mango.Web.App.Models;
using Mango.Web.App.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductIndex()
        {
            // Create a new instance of the main object and retrive all Products.
            List<ProductDto>? list = new();
            ResponseDto? response = await _productService.GetAllProductsAsync();
            // Validate if response is success and fill the "list" object.
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            // Return view with model.
            return View(list);
        }

        [HttpGet]
        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
			// This line can help us to validate the data annotation of a model.
			if (ModelState.IsValid)
            {
                ResponseDto? response = await _productService.CreateProductAsync(model);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product created successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductEdit(int id)
        {
            ResponseDto? response = await _productService.GetProductByIdAsync(id);

            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            // This line can help us to validate the data annotation of a model.
            if (ModelState.IsValid)
            {
				ResponseDto? response = await _productService.UpdateProductAsync(model);

				if (response != null && response.IsSuccess)
				{
					TempData["success"] = "Product updated successfully";
					return RedirectToAction(nameof(ProductIndex));
				}
				else
				{
					TempData["error"] = response?.Message;
				}
			}
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDelete(int id)
        {
            ResponseDto? response = await _productService.GetProductByIdAsync(id);

            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            ResponseDto? response = await _productService.DeleteProductAsync(model.ProductId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(model);
        }
    }
}
