namespace Mango.Services.Email.Web.Api.Messaging
{
    /// <summary>
    /// This interface defines function to process any recieved request (queue) from Azure Service Bus resource.
    /// </summary>
    public interface IAzureServiceBusConsumer
    {
        /// <summary>
        /// Function to start a processor for Azure Service Bus request.
        /// </summary>
        /// <returns>Task.</returns>
        Task Start();

        /// <summary>
        /// Function to stop a processor for Azure Service Bus request.
        /// </summary>
        /// <returns>Task.</returns>
        Task Stop();
    }
}
