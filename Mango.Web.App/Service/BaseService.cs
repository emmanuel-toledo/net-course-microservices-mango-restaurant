using Mango.Web.App.Models;
using Mango.Web.App.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static Mango.Web.App.Utility.SD;

namespace Mango.Web.App.Service
{
    /// <summary>
    /// Class that implements the "IBaseService" interface.
    /// </summary>
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ITokenProvider _tokenProvider;

        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        /// <summary>
        /// Execute a REST Service request to an API.
        /// </summary>
        /// <param name="requestDto">Request configuration model.</param>
        /// <param name="withBearer">Flag to validate if the request requires access token.</param>
        /// <returns>Response model.</returns>
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                // Initialize a new "HttpClient".
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();

                if(requestDto.ContentType == ContentType.MultipartFormData)
                {
                    // Accept any media type and subtype for the request.
					message.Headers.Add("Accept", "*/*");
				}
                else
                {
					message.Headers.Add("Accept", "application/json");
				}

                // Set access Token if request needs.
                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                // Configure the request details.
                message.RequestUri = new Uri(requestDto.Url);

                // Check if the request need multipar form data (a file).
                if(requestDto.ContentType == ContentType.MultipartFormData)
                {
                    // Loop inside all the properties of "requestDto" variable.
                    var content = new MultipartFormDataContent();
                    foreach(var prop in requestDto.Data.GetType().GetProperties())
                    {
                        // Get the value of the current property in the loop.
                        var value = prop.GetValue(requestDto.Data);
                        // Check if the property's value is of type FormFile (or IFormFile).
                        if (value is FormFile)
                        {
                            // Convert the value in a FormFile.
                            var file = (FormFile)value;
                            // If the file is not null.
                            if (file != null)
                            {
                                // Add the file value in the content.
                                content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                            }
                        }
                        else
                        {
                            // If the property is not FormFile type we add a new string value to the content.
                            content.Add(new StringContent(value == null ? "" : value.ToString()), prop.Name);
						}
                    }
                    message.Content = content;
                }
                else
                {
                    // If the request does not need multipar form data we send a new application/json request.
					if (requestDto.Data != null)
					{
						message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
					}
				}

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                // Get the response of the request.
                HttpResponseMessage? apiResponse = null;
                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal server error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto()
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
                return dto;
            }
        }
    }
}
