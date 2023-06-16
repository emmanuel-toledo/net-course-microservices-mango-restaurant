using Microsoft.AspNetCore.Mvc;
using Mango.Services.Auth.Web.Api.Models.Dto;
using Mango.Services.Coupon.Web.Api.Models.Dto;
using Mango.Services.Auth.Web.Api.Service.IService;

namespace Mango.Services.Auth.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        protected ResponseDto _response;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new ResponseDto();
        }

        /// <summary>
        /// Function to add a new user in the database for login.
        /// <para>
        /// For the registration the property "role" doesn't do nothing.
        /// </para>
        /// </summary>
        /// <param name="model">User information.</param>
        /// <returns>Success response.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if(!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            return Ok(_response);
        }

        /// <summary>
        /// Function to login a user in the application and generate access token.
        /// </summary>
        /// <param name="model">User login information.</param>
        /// <returns>Success response.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if(loginResponse.User is null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        /// <summary>
        /// Function to assign a new role to a user.
        /// <para>
        /// We only will need "role" and "email" properties.
        /// </para>
        /// </summary>
        /// <param name="model">User information.</param>
        /// <returns>Flag to validate if role was assigned.</returns>
        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            return Ok(_response);
        }
    }
}
