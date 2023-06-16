using Mango.Web.App.Models;
using Mango.Web.App.Service.IService;
using Mango.Web.App.Utility;

namespace Mango.Web.App.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Function to login an user into the application.
        /// </summary>
        /// <param name="loginRequestDto">Login request model.</param>
        /// <returns>Response model instance.</returns>
        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/login"
            }, false);
        }

        /// <summary>
        /// Function to create a new user inside the application.
        /// </summary>
        /// <param name="registrationRequestDto">Registration request model.</param>
        /// <returns>Response model instance.</returns>
        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/register"
            }, false);
        }

        /// <summary>
        /// Function to set a new rol to a user into the application.
        /// </summary>
        /// <param name="registrationRequestDto">Registration request model.</param>
        /// <returns>Response model instance.</returns>
        public async Task<ResponseDto?> AssingnRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/assignrole"
            });
        }
    }
}
