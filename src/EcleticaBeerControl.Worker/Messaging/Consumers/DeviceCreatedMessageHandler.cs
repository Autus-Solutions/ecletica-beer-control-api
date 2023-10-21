using RabbitMQ.Client.Core.DependencyInjection.MessageHandlers;
using RabbitMQ.Client.Core.DependencyInjection.Models;
using Serilog;

namespace EcleticaBeerControl.Worker.Messaging.Consumers
{
    public class DeviceCreatedMessageHandler : IAsyncMessageHandler
    {
        public Task Handle(MessageHandlingContext context, string matchingRoute)
        {
            Log.Information("Message Arrived: {MatchingRoute}", matchingRoute);
            return Task.CompletedTask;
        }
    }
}
