using EcleticaBeerControl.Domain.Dtos;
using EcleticaBeerControl.Worker.Mqtt;
using MQTTnet.Client;
using Newtonsoft.Json;
using RabbitMQ.Client.Core.DependencyInjection.MessageHandlers;
using RabbitMQ.Client.Core.DependencyInjection.Models;
using Serilog;
using System.Text;

namespace EcleticaBeerControl.Worker.Messaging.Handlers
{
    public class DeviceCreatedMessageHandler : IAsyncMessageHandler
    {
        private readonly IMqttClient _mqttClient;

        public DeviceCreatedMessageHandler(IMqttClient mqttClient)
        {
            _mqttClient = mqttClient;
        }

        public async Task Handle(MessageHandlingContext context, string matchingRoute)
        {
            Log.Information("Message Arrived: {MatchingRoute}", matchingRoute);

            var deviceDto = JsonConvert.DeserializeObject<DeviceDto>(Encoding.UTF8.GetString(context.Message.Body.ToArray()));

            await _mqttClient.PublishAsync(new MQTTnet.MqttApplicationMessage
            {
                Topic = RouteKeys.ApplicationDeviceHandshakeRoute(deviceDto!.Identifier),
                ContentType = "text/plain",
                PayloadSegment = Encoding.UTF8.GetBytes(deviceDto!.BreweryId.ToString())
            });
        }
    }
}
