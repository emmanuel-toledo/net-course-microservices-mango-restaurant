
using Mango.Services.Reward.Web.Api.Message;

namespace Mango.Services.Reward.Web.Api.Services
{
    /// <summary>
    /// This interface defines all the functions to store a reward in the database.
    /// </summary>
    public interface IRewardService
    {
        /// <summary>
        /// Function to save a reward inside the database.
        /// </summary>
        /// <param name="rewardsMessage">Reward information.</param>
        /// <returns>Async task.</returns>
        Task UpdateRewards(RewardsMessage rewardsMessage);
    }
}
