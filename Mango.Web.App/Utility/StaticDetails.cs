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
        /// Api method types for a service.
        /// </summary>
        public enum ApiType {
            GET,
            POST, 
            PUT, 
            DELETE
        }
    }
}
