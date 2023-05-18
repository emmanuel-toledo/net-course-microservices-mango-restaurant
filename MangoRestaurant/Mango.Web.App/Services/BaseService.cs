using Mango.Web.App.Models;
using Mango.Web.App.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Mango.Web.App.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDto response { get; set; }

        // Utilizamos la Inyección de dependencias para establecer un valor a nuestra propiedad httpClient.
        public IHttpClientFactory httpClient { get; set; }

        // Utilizamos la Inyección de dependencias para establecer un valor a nuestra propiedad httpClient.
        public BaseService(IHttpClientFactory httpClient)
        {
            this.response = new ResponseDto();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest request)
        {
            try
            {
                var client = httpClient.CreateClient("MangoAPI"); // Declaramos un cliente HTTP llamado MangoAPI.
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(request.URL);
                client.DefaultRequestHeaders.Clear();
                if(request.Data != null)
                {
                    message.Content = new StringContent(
                        JsonConvert.SerializeObject(request.Data), 
                        Encoding.UTF8, 
                        "application/json"
                    );
                }

                if (!string.IsNullOrEmpty(request.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.AccessToken);
                }

                HttpResponseMessage response = null;
                switch(request.RequestType)
                {
                    case SD.RequestType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.RequestType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.RequestType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                response = await client.SendAsync(message);
                var content = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<T>(content);
                return responseDto;
            } catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    Message = "Error",
                    ErrorMessages = new List<string> { ex.Message },
                    IsSuccess = false,
                };
                var res = JsonConvert.SerializeObject(dto);
                var errorResponseDto = JsonConvert.DeserializeObject<T>(res);
                return errorResponseDto;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true); // Garbage Collection (GC). Limpiamos los recursos utilizados.
        }
    }
}
