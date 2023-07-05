namespace Mango.Web.App.Utility
{
    /// <summary>
    /// Static details class for project.
    /// </summary>
    public class SD
    {
        /// <summary>
        /// Get and set the base url address for Coupon Service.
        /// </summary>
        public static string CouponAPIBase { get; set; }

        /// <summary>
        /// Get and set the base url address for Auth Service.
        /// </summary>
        public static string AuthAPIBase { get; set; }

        /// <summary>
        /// Get and set the base url address for Product Service.
        /// </summary>
        public static string ProductAPIBase { get; set; }

        /// <summary>
        /// Get and set the base url address for Shopping cart Service.
        /// </summary>
        public static string ShoppingCartAPIBase { get; set; }

        /// <summary>
        /// Get and set the base url address for Order Service.
        /// </summary>
        public static string OrderAPIBase { get; set; }

        /// <summary>
        /// Const admin role variable.
        /// </summary>
        public const string RoleAdmin = "ADMIN";

        /// <summary>
        /// Const customer role variable.
        /// </summary>
        public const string RoleCustomer = "CUSTOMER";

        /// <summary>
        /// Const token cookie variable.
        /// </summary>
        public const string TokenCookie = "JWTTOKEN";

        /// <summary>
        /// Api method types for a service.
        /// </summary>
        public enum ApiType {
            GET,
            POST, 
            PUT, 
            DELETE
        }

        /// <summary>
        /// Pending status for payment.
        /// </summary>
        public const string Status_Pending = "Pending";

        /// <summary>
        /// Approve status for payment.
        /// </summary>
        public const string Status_Approved = "Approved";

        /// <summary>
        /// Ready for pickup status for payment.
        /// </summary>
        public const string Status_ReadyForPickup = "ReadyForPickup";

        /// <summary>
        /// Completed status for payment.
        /// </summary>
        public const string Status_Completed = "Completed";

        /// <summary>
        /// Refunded status for payment.
        /// </summary>
        public const string Status_Refunded = "Refunded";

        /// <summary>
        /// Cancelled status for payment.
        /// </summary>
        public const string Status_Cancelled = "Cancelled";

        /// <summary>
        /// Enumerable for content types.
        /// </summary>
        public enum ContentType
        {
            Json,
            MultipartFormData
        }
    }
}
