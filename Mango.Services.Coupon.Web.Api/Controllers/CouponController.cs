using AutoMapper;
using Mango.Services.Coupon.Web.Api.Data;
using Mango.Services.Coupon.Web.Api.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.Coupon.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public CouponController(AppDbContext db, IMapper mapper)
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
                // Get list of coupons from DB.
                IEnumerable<Models.Coupon> objList = _db.Coupons.ToList();
                // Use automapper to convert Coupon model to CouponDto.
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
            }
            catch(Exception ex)
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
                // Get the first elemento of the Coupon table with specific id.
                Models.Coupon obj = _db.Coupons.First(x => x.CouponId == id);
                // Use automapper to convert Coupon model to CouponDto.
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto Get([FromRoute] string code)
        {
            try
            {
                // Get the first elemento of the Coupon table with specific id.
                Models.Coupon obj = _db.Coupons.First(x => x.CouponCode.ToLower() == code.ToLower());
                // Use automapper to convert Coupon model to CouponDto.
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                // Convert couponDto to coupon model and insert it to db.
                var coupon = _mapper.Map<Models.Coupon>(couponDto);
                _db.Coupons.Add(coupon);
                _db.SaveChanges();
                // Use automapper to convert Coupon model to CouponDto.
                _response.Result = couponDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                // Convert couponDto to coupon model and insert it to db.
                var coupon = _mapper.Map<Models.Coupon>(couponDto);
                _db.Coupons.Update(coupon);
                _db.SaveChanges();
                // Use automapper to convert Coupon model to CouponDto.
                _response.Result = couponDto;
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
        public ResponseDto Delete([FromRoute] int id)
        {
            try
            {
                // Get the first elemento of the Coupon table with specific id.
                Models.Coupon obj = _db.Coupons.First(x => x.CouponId == id);
                // Remove coupon and save changes.
                _db.Coupons.Remove(obj);
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
