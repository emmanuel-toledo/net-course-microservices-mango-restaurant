namespace Mango.Integration.MessageBus
{
    /// <summary>
    /// This interface defines the methods to connect to Azure Service Bus.
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// Function to send a message to an Azure Service Bus resource.
        /// </summary>
        /// <param name="message">Message to sent</param>
        /// <param name="topic_queue_name">Queue name from azure service bus resource.</param>
        /// <returns>Async task.</returns>
        Task PublishMessage(object message, string topic_queue_name);
    }
}
