namespace Mango.Services.Order.Web.Api.Models.Dto
{
    /// <summary>
    /// This class contains the response properties for a request.
    /// </summary>
    public class ResponseDto
    {
        /// <summary>
        /// Get and set the request result object.
        /// </summary>
        public object? Result { get; set; }

        /// <summary>
        /// Get and set the request success flag.
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Get and set the request message.
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
