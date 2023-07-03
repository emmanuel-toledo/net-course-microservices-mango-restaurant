namespace Mango.Services.Order.Web.Api.Models.Dto
{
    /// <summary>
    /// This class contains the properties for a reward in a database.
    /// </summary>
    public class RewardsDto
    {
        /// <summary>
        /// Get and set the user unique identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Get and set the reward activity.
        /// </summary>
        public int RewardsActivity { get; set; }

        /// <summary>
        /// Get and set the order unique identifier.
        /// </summary>
        public int OrderId { get; set; }
    }
}
