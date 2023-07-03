namespace Mango.Services.Reward.Web.Api.Message
{
    /// <summary>
    /// This class contains the properties for a reward message in azure service bus.
    /// </summary>
    public class RewardsMessage
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
