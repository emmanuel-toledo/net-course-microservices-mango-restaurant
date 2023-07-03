using Azure.Messaging.ServiceBus;
using Mango.Services.Email.Web.Api.Message;
using Mango.Services.Email.Web.Api.Models.Dto;
using Mango.Services.Email.Web.Api.Services;
using Newtonsoft.Json;
using System.Text;

namespace Mango.Services.Email.Web.Api.Messaging
{
    /// <summary>
    /// This class will recieve any request (queue) from Azure Service Bus resource.
    /// </summary>
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;

        private readonly string emailCartQueue;

        private readonly string registerUserQueue;

        private readonly string orderCreatedTopic;

        private readonly string orderCreatedEmailSubscription;

        private readonly IConfiguration _configuration;

        private readonly EmailService _emailService;

        private ServiceBusProcessor _emailCartProcessor;

        private ServiceBusProcessor _registerUserProcessor;

        private ServiceBusProcessor _emailOrderPlacedProcessor;

        public AzureServiceBusConsumer(IConfiguration configuration, EmailService emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
            
            // Set variables values from appsettings.json file.
            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            emailCartQueue = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue");
            registerUserQueue = _configuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue");
            orderCreatedTopic = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic");
            orderCreatedEmailSubscription = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreated_Email_Subscription");

            // Create client for Azure Service Bus resource.
            var client = new ServiceBusClient(serviceBusConnectionString);
            // Create the processor for a Queue.
            _emailCartProcessor = client.CreateProcessor(emailCartQueue);
            _registerUserProcessor = client.CreateProcessor(registerUserQueue);
            _emailOrderPlacedProcessor = client.CreateProcessor(orderCreatedTopic, orderCreatedEmailSubscription);
        }

        /// <summary>
        /// Function to start all the processors for Azure Service Bus request.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task Start()
        {
            _emailCartProcessor.ProcessMessageAsync += OnEmailCartRequestReceived;
            _emailCartProcessor.ProcessErrorAsync += ErrorHandler;
            await _emailCartProcessor.StartProcessingAsync();

            _registerUserProcessor.ProcessMessageAsync += OnUserRegisterRequestReceived;
            _registerUserProcessor.ProcessErrorAsync += ErrorHandler;
            await _registerUserProcessor.StartProcessingAsync();

            _emailOrderPlacedProcessor.ProcessMessageAsync += OnOrderPlacedRequestReceived;
            _emailOrderPlacedProcessor.ProcessErrorAsync += ErrorHandler;
            await _emailOrderPlacedProcessor.StartProcessingAsync();
        }

        /// <summary>
        /// Function to stop all the processors for Azure Service Bus request.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task Stop()
        {
            await _emailCartProcessor.StopProcessingAsync();
            await _emailCartProcessor.DisposeAsync();

            await _registerUserProcessor.StopProcessingAsync();
            await _registerUserProcessor.DisposeAsync();

            await _emailOrderPlacedProcessor.StopProcessingAsync();
            await _emailOrderPlacedProcessor.DisposeAsync();
        }

        /// <summary>
        /// Function to manage all the request in email shopping cart queue from azure service bus.
        /// </summary>
        /// <param name="args">Process message event arguments.</param>
        /// <returns>Task.</returns>
        private async Task OnEmailCartRequestReceived(ProcessMessageEventArgs args)
        {
            // This code is executed after to receive a message for the selected queue.
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            // This is the content from the queue of Azure Service Bus.
            CartDto objMessage = JsonConvert.DeserializeObject<CartDto>(body);
            try
            {
                // Try to log email.
                await _emailService.EmailCartAndLog(objMessage);
                await args.CompleteMessageAsync(args.Message);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Function to manage all the request in register user queue from azure service bus.
        /// </summary>
        /// <param name="args">Process message event arguments.</param>
        /// <returns>Task.</returns>
        private async Task OnUserRegisterRequestReceived(ProcessMessageEventArgs args)
        {
            // This code is executed after to receive a message for the selected queue.
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            // This is the content from the queue of Azure Service Bus.
            string email = JsonConvert.DeserializeObject<string>(body);
            try
            {
                // Try to log email.
                await _emailService.RegisterUserEmailAndLog(email);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Function to manage all the request in register new order from azure service bus.
        /// </summary>
        /// <param name="args">Process message event arguments.</param>
        /// <returns>Task.</returns>
        private async Task OnOrderPlacedRequestReceived(ProcessMessageEventArgs args)
        {
            // This code is executed after to receive a message for the selected queue.
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            // This is the content from the queue of Azure Service Bus.
            var rewardsMessage = JsonConvert.DeserializeObject<RewardsMessage>(body);
            try
            {
                // Try to log email for order placed.
                await _emailService.LogOrderPlaced(rewardsMessage);
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
