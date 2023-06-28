using Mango.Services.Email.Web.Api.Messaging;

namespace Mango.Services.Email.Web.Api.Microsoft.Extensions
{
    /// <summary>
    /// This class contains any extension method to Application Builder.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Instance of service Azure Service Bus Consumer.
        /// </summary>
        private static IAzureServiceBusConsumer ServiceBusConsumer { get; set; }

        /// <summary>
        /// Function to use the implementation of Azure Service Bus for Application Builder.
        /// <para>
        /// Once that the applization is started will execute "OnStart" method to start and receive any request from Azure Service Bus.
        /// </para>
        /// <para>
        /// Once that the applization is stoppoing will execute "OnStop" method to stop and don't receive anymore any request from Azure Service Bus.
        /// </para>
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            ServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLife.ApplicationStarted.Register(OnStart);
            hostApplicationLife.ApplicationStopping.Register(OnStop);

            return app;
        }

        /// <summary>
        /// Function to call Stop function from Azure Service Bus Consumer service.
        /// </summary>
        private static void OnStop()
        {
            ServiceBusConsumer.Stop();
        }

        /// <summary>
        /// Function to call Start function from Azure Service Bus Consumer service.
        /// </summary>
        private static void OnStart()
        {
            ServiceBusConsumer.Start();
        }
    }
}
