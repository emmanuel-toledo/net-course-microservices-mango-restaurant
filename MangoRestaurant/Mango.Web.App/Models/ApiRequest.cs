using static Mango.Web.App.SD;

namespace Mango.Web.App.Models
{
    public class ApiRequest
    {
        public RequestType RequestType { get; set; } = RequestType.GET;

        public string URL { get; set; }

        public object Data { get; set; }

        public string AccessToken { get; set; }
    }
}
