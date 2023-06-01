namespace Mango.Services.Coupon.Web.Api.Models.Dto
{
    /// <summary>
    /// This is the main response object to each call to the service.
    /// </summary>
    public class ResponseDto
    {
        /// <summary>
        /// Get and set the generic result of the request.
        /// </summary>
        public object? Result { get; set; }

        /// <summary>
        /// Get and set the is success flag.
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Get and set the error message.
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
