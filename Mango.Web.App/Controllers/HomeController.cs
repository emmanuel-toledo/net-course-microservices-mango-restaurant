using Mango.Web.App.Models;
using Mango.Web.App.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace Mango.Web.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductService _productService;

        private readonly ICartService _cartService;

		public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
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

        [Authorize]
        public async Task<IActionResult> ProductDetails(int id)
        {
            // Create a new instance of the main object and retrive all Products.
            ProductDto model = new();
            ResponseDto? response = await _productService.GetProductByIdAsync(id);
            // Validate if response is success and fill the "list" object.
            if (response != null && response.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            // Return view with model.
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ActionName("ProductDetails")]
        public async Task<IActionResult> ProductDetails(ProductDto productDto)
        {
            // Add a new product to the sopping cart.
            // Create new instance of cart dto.
            CartDto cartDto = new()
            {
                CartHeader = new()
                {
                    UserId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value,
                }
            };
            CartDetailsDto cartDetailsDto = new()
            {
                Count = productDto.Count,
                ProductId = productDto.ProductId,
            };
            cartDto.CartDetails = new List<CartDetailsDto>() { cartDetailsDto };

            // Upsert to the database.
            ResponseDto? response = await _cartService.UpsertCartAsync(cartDto);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Item has been added to the Sopping Cart";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            // Return view with model.
            return View(productDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}