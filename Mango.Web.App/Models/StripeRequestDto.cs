namespace Mango.Web.App.Models
{
    /// <summary>
    /// This model contains the required properties for Stripe payment integration.
    /// </summary>
    public class StripeRequestDto
    {
        /// <summary>
        /// Get and set session URL to know who execute the payment action.
        /// </summary>
        public string? StripeSessionUrl { get; set; }

        /// <summary>
        /// Get and set session id to know who execute the payment action.
        /// </summary>
        public string? StripeSessionId { get; set; }

        /// <summary>
        /// Get and set the redirect url when a payment was successful.
        /// </summary>
        public string ApproveUrl { get; set; }

        /// <summary>
        /// Get and set the redirect url when a payment was canceled.
        /// </summary>
        public string CancelUrl { get; set; }

        /// <summary>
        /// Get and set the model with the information of the product(s) to pay.
        /// </summary>
        public OrderHeaderDto OrderHeader { get; set; }
    }
}
