using Azure.Messaging.ServiceBus;
using Mango.Services.Reward.Web.Api.Message;
using Mango.Services.Reward.Web.Api.Services;
using Newtonsoft.Json;
using System.Text;

namespace Mango.Services.Reward.Web.Api.Messaging
{
    /// <summary>
    /// This class will recieve any request (queue) from Azure Service Bus resource.
    /// </summary>
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;

        private readonly string orderCreatedTopic;

        private readonly string orderCreatedRewardsSubscription;

        private readonly IConfiguration _configuration;

        private readonly RewardService _rewardService;

        private ServiceBusProcessor _rewardProcessor;

        public AzureServiceBusConsumer(IConfiguration configuration, RewardService rewardService)
        {
            _configuration = configuration;
            _rewardService = rewardService;

            // Set variables values from appsettings.json file.
            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            orderCreatedTopic = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic");
            orderCreatedRewardsSubscription = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreated_Rewards_Subscription");

            // Create client for Azure Service Bus resource.
            var client = new ServiceBusClient(serviceBusConnectionString);
            // Create the processor for a Subcription of a Topic.
            _rewardProcessor = client.CreateProcessor(orderCreatedTopic, orderCreatedRewardsSubscription);
        }

        /// <summary>
        /// Function to start all the processors for Azure Service Bus request.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task Start()
        {
            _rewardProcessor.ProcessMessageAsync += OnNewOrderRewardsRequestReceived;
            _rewardProcessor.ProcessErrorAsync += ErrorHandler;
            await _rewardProcessor.StartProcessingAsync();
        }

        /// <summary>
        /// Function to stop all the processors for Azure Service Bus request.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task Stop()
        {
            await _rewardProcessor.StopProcessingAsync();
            await _rewardProcessor.DisposeAsync();
        }

        /// <summary>
        /// Function to manage all the request in "OrderCreatedRewardsUpdate" subcription inside of "OrderCreated" topic.
        /// </summary>
        /// <param name="args">Process message event arguments.</param>
        /// <returns>Task.</returns>
        private async Task OnNewOrderRewardsRequestReceived(ProcessMessageEventArgs args)
        {
            // This code is executed after to receive a message for the selected queue.
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            // This is the content from the queue of Azure Service Bus.
            var objMessage = JsonConvert.DeserializeObject<RewardsMessage>(body);
            try
            {
                // Try to log email.
                await _rewardService.UpdateRewards(objMessage);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// This method manage any error from Azure Service Bus. 
        /// <para>
        /// You could add any logic but in this case we only show the error message inside the console.
        /// </para>
        /// </summary>
        /// <param name="args">Process error event arguments.</param>
        /// <returns>Task.</returns>
        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
