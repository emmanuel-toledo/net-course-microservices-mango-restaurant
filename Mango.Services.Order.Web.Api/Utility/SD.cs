namespace Mango.Services.Order.Web.Api.Utility
{
    /// <summary>
    /// Static Details class for the api.
    /// </summary>
    public static class SD
    {
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


        public const string RoleAdmin = "ADMIN";

        public const string RoleCustomer = "CUSTOMER";
    }
}
