using AutoMapper;
using Mango.Integration.MessageBus;
using Mango.Services.Order.Web.Api.Data;
using Mango.Services.Order.Web.Api.Models;
using Mango.Services.Order.Web.Api.Models.Dto;
using Mango.Services.Order.Web.Api.Service.IService;
using Mango.Services.Order.Web.Api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;

namespace Mango.Services.Order.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private ResponseDto _response;

        private readonly IMapper _mapper;

        private readonly AppDbContext _db;

        private readonly IProductService _productService;

        private readonly IMessageBus _messageBus;

        private readonly IConfiguration _configuration;

        public OrderController(IMapper mapper, AppDbContext db, IProductService productService, IMessageBus messageBus, IConfiguration configuration)
        {
            _mapper = mapper;
            _db = db;
            _response = new();
            _productService = productService;
            _messageBus = messageBus;
            _configuration = configuration;
        }

        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ResponseDto> CreateOrder([FromBody] CartDto cartDto)
        {
            try
            {
                OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto.CartHeader);
                orderHeaderDto.OrderTime = DateTime.Now;
                orderHeaderDto.Status = SD.Status_Pending;
                orderHeaderDto.OrderDetails = _mapper.Map<IEnumerable<OrderDetailsDto>>(cartDto.CartDetails);

                OrderHeader orderCreated = _db.OrderHeaders.Add(_mapper.Map<OrderHeader>(orderHeaderDto)).Entity;

                await _db.SaveChangesAsync();

                orderHeaderDto.OrderHeaderId = orderCreated.OrderHeaderId;

                _response.Result = orderHeaderDto;
                _response.IsSuccess = true;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Authorize]
        [HttpPost("CreateStripeSession")]
        public async Task<ResponseDto> CreateStripeSession([FromBody] StripeRequestDto stripeRequestDto)
        {
            try
            {
                // Create main stripe option model.
                var options = new SessionCreateOptions
                {
                    SuccessUrl = stripeRequestDto.ApproveUrl,
                    CancelUrl = stripeRequestDto.CancelUrl,
                    LineItems = new(),
                    Mode = "payment",
                };

                // Create object to discount using a coupon in stripe.
                var discountsObj = new List<SessionDiscountOptions>()
                {
                    new SessionDiscountOptions()
                    {
                        Coupon = stripeRequestDto.OrderHeader.Couponcode
                    }
                };

                // Loop for each product.
                foreach(var item in stripeRequestDto.OrderHeader.OrderDetails)
                {
                    // Create session line item options.
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(Convert.ToDouble(item.Price) * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                // Only payments with coupons will apply a discount.
                if(stripeRequestDto.OrderHeader.Discount > 0)
                {
                    options.Discounts = discountsObj;
                }

                // Create stripe service.
                var service = new SessionService();
                Session session = service.Create(options); // We can store a stripe session. This is not a .NET session.
                stripeRequestDto.StripeSessionUrl = session.Url;

                // Store the stripe session id in the database.
                OrderHeader orderHeader = _db.OrderHeaders.First(u => u.OrderHeaderId == stripeRequestDto.OrderHeader.OrderHeaderId);
                orderHeader.StripeSessionId = session.Id;
                _db.SaveChanges();

                _response.Result = stripeRequestDto;

            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Authorize]
        [HttpPost("ValidateStripeSession")]
        public async Task<ResponseDto> ValidateStripeSession([FromBody] int orderHeaderId)
        {
            try
            {
                // Search for order header in db.
                OrderHeader orderHeader = _db.OrderHeaders.First(u => u.OrderHeaderId == orderHeaderId);

                // Create session service and search for a session using id.
                var service = new SessionService();
                Session session = service.Get(orderHeader.StripeSessionId);

                // Create payment intent service and search for a payment intent id.
                var paymentIntentService = new PaymentIntentService();
                PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

                // Validate the status.
                if(paymentIntent.Status == "succeeded")
                {
                    // Then payment was successful.
                    orderHeader.PaymentIntentId = paymentIntent.Id;
                    orderHeader.Status = SD.Status_Approved;
                    _db.SaveChanges();
                    _response.Result = _mapper.Map<OrderHeaderDto>(orderHeader);
                }

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
