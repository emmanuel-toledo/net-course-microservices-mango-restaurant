using Mango.Web.App.Models;
using Mango.Web.App.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.App.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> CouponIndex()
        {
            // Create a new instance of the main object and retrive all coupons.
            List<CouponDto>? list = new();
            ResponseDto? response = await _couponService.GetAllCouponsAsync();
            // Validate if response is success and fill the "list" object.
            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            } 
            else
            {
                TempData["error"] = response?.Message;
            }
            // Return view with model.
            return View(list);
        }

        [HttpGet]
        public IActionResult CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(model);

                if(response != null && response.IsSuccess)
                {
                    TempData["success"] = "Coupon created successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
				else
				{
					TempData["error"] = response?.Message;
				}
			}
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CouponDelete(int id)
        {
            ResponseDto? response = await _couponService.GetCouponByIdAsync(id);

            if (response != null && response.IsSuccess)
            {
                CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(model);
            }
			else
			{
				TempData["error"] = response?.Message;
			}
			return NotFound();
        }

		[HttpPost]
		public async Task<IActionResult> CouponDelete(CouponDto model)
        {
			ResponseDto? response = await _couponService.DeleteCouponAsync(model.CouponId);

			if (response != null && response.IsSuccess)
			{
                TempData["success"] = "Coupon deleted successfully";
                return RedirectToAction(nameof(CouponIndex));
			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return View(model);
		}

	}
}
