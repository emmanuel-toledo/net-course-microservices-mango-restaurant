namespace Mango.Services.Reward.Web.Api.Models
{
    /// <summary>
    /// This class contains the properties for a reward in a database.
    /// </summary>
    public class Rewards
    {
        /// <summary>
        /// Get and set a reward unique identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get and set the user unique identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Get and set the creation date.
        /// </summary>
        public DateTime RewardsDate { get; set; }

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
