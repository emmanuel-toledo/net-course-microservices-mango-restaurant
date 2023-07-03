using Mango.Web.App.Models;
using Mango.Web.App.Service.IService;
using Mango.Web.App.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Mango.Web.App.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        [HttpPost]
        [ActionName("Checkout")]
        public async Task<IActionResult> Checkout(CartDto cartDto)
        {
            // We reload the user's cart.
            CartDto cart = await LoadCartDtoBasedOnLoggedInUser();
            // Set users values from request body.
            cart.CartHeader.Phone = cartDto.CartHeader.Phone;
            cart.CartHeader.Email = cartDto.CartHeader.Email;
            cart.CartHeader.Name = cartDto.CartHeader.Name;
            // Create new order in the database.
            var response = await _orderService.CreateOrderAsync(cart);
            OrderHeaderDto orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
            
            if(response != null && response.IsSuccess)
            {
                // Redirect to gateway to make the payment.
                // Payment integration with stripe library.
                var domain = $"{Request.Scheme}://{Request.Host.Value}/";
                StripeRequestDto stripeRequestDto = new()
                {
                    ApproveUrl = $"{domain}cart/Confirmation?orderId={orderHeaderDto.OrderHeaderId}",
                    CancelUrl = $"{domain}cart/Checkout",
                    OrderHeader = orderHeaderDto
                };
                var stripeResponse = await _orderService.CreateStripeSession(stripeRequestDto);
                StripeRequestDto stripeResponseResult = JsonConvert.DeserializeObject<StripeRequestDto>(Convert.ToString(stripeResponse.Result));
                
                Response.Headers.Add("Location", stripeResponseResult.StripeSessionUrl);
                return new StatusCodeResult(303);
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Confirmation(int orderId)
        {
            // You can create a queue to validate a payment status, that will require a new microservice.
            // This is another option.
            var response = await _orderService.ValidateStripeSession(orderId);
            if (response != null && response.IsSuccess)
            {
                OrderHeaderDto orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
                if(orderHeaderDto.Status == SD.Status_Approved)
                {
                    return View(orderId);
                }
                return RedirectToAction(nameof(CartIndex));
            }
            // You also can modify the view and redirect to an error page.
            return View(orderId);
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            var response = await _cartService.RemoveFromCartAsync(cartDetailsId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            cartDto.CartDetails = new List<CartDetailsDto>();
            var response = await _cartService.ApplyCouponAsync(cartDto);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            cartDto.CartDetails = new List<CartDetailsDto>();
            cartDto.CartHeader.Couponcode = "";
            var response = await _cartService.RemoveCouponAsync(cartDto);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        public async Task<IActionResult> EmailCart(CartDto cartDto)
        {
            // Load the cart of a user from the database.
            CartDto cart = await LoadCartDtoBasedOnLoggedInUser();
            // Set user's email address.
            cart.CartHeader.Email = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Email)?.FirstOrDefault()?.Value;
            // Call to http service to send a queue to Service Bus in Azure.
            var response = await _cartService.EmailCart(cart);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Email will be processed and sent shortly.";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {
            // Retrive the user unique identifier from claims of the session.
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            // Get the cart from the user.
            var response = await _cartService.GetCartByUserIdAsync(userId);
            if(response != null && response.IsSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto;
            }
            return new();
        }
    }
}
