using Mango.Services.Reward.Web.Api.Data;
using Mango.Services.Reward.Web.Api.Message;
using Mango.Services.Reward.Web.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.Reward.Web.Api.Services
{
    /// <summary>
    /// This class implements all the functions to store a reward in the database.
    /// </summary>
    public class RewardService : IRewardService
    {
        private DbContextOptions<AppDbContext> _dbOptions;

        public RewardService(DbContextOptions<AppDbContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        /// <summary>
        /// Function to save a reward inside the database.
        /// </summary>
        /// <param name="rewardsMessage">Reward information.</param>
        /// <returns>Async task.</returns>
        public async Task UpdateRewards(RewardsMessage rewardsMessage)
        {
            try
            {
                Rewards rewards = new()
                {
                    OrderId = rewardsMessage.OrderId,
                    RewardsActivity = rewardsMessage.RewardsActivity,
                    UserId = rewardsMessage.UserId,
                    RewardsDate = DateTime.Now
                };

                // Create a new instance of AppDbContext to access to the database using EF Core.
                await using var _db = new AppDbContext(_dbOptions);

                await _db.Rewards.AddAsync(rewards);
                await _db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
            }
        }
    }
}
