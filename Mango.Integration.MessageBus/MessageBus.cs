using System.Text;
using Newtonsoft.Json;
using Azure.Messaging.ServiceBus;

namespace Mango.Integration.MessageBus
{
    /// <summary>
    /// This class implements the methods to connect to Azure Service Bus.
    /// </summary>
    public class MessageBus : IMessageBus
    {
        /// <summary>
        /// Connection string to Azure Service Bus (you can get it in the Shared Access Policies section).
        /// </summary>
        private string connectionString = "Endpoint=sb://toledo-mangoweb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=0i2T6A57uBah1BojyjdQV8BTxxcAqXILM+ASbFcfk9Q=";

        /// <summary>
        /// Function to send a message to an Azure Service Bus resource.
        /// </summary>
        /// <param name="message">Message to sent</param>
        /// <param name="topic_queue_name">Queue name from azure service bus resource.</param>
        /// <returns>Async task.</returns>
        public async Task PublishMessage(object message, string topic_queue_name)
        {
            await using var client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(topic_queue_name);

            var jsonMessage = JsonConvert.SerializeObject(message);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            await sender.SendMessageAsync(finalMessage);

            await client.DisposeAsync();
        }
    }
}
