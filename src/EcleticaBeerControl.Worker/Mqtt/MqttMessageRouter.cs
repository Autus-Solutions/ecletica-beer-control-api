using EcleticaBeerControl.Infrastructure.Mqtt;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using Serilog;
using System.Text;

namespace EcleticaBeerControl.Worker.Mqtt
{
    public sealed class MqttMessageRouter : IMqttMessageRouter
    {
        private readonly IMqttClient _mqttClient;
        private readonly IEnumerable<IMqttRouteHandler> _mqttRoutes;

        public MqttMessageRouter(IMqttClient mqttClient, IEnumerable<IMqttRouteHandler> mqttRoutes)
        {
            mqttClient.ApplicationMessageReceivedAsync += Instance_ApplicationMessageReceivedAsync;
            _mqttClient = mqttClient;
            _mqttRoutes = mqttRoutes;
        }

        public async Task Initialize(CancellationToken cancellationToken = default)
        {
            Log.Information("MqttMessageRouter initializing ...");

            while(true)
            {
                if (_mqttClient.IsConnected)
                {
                    var topicFilters = _mqttRoutes.Select(r => new MqttTopicFilterBuilder()
                        .WithTopic(r.Route)
                        .WithAtLeastOnceQoS()
                    .Build());

                    await Task.WhenAll(topicFilters.Select(tf => _mqttClient.SubscribeAsync(tf, cancellationToken)));
                    
                    break;
                }

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            };

            Log.Information("MqttMessageRouter initialized");
        }

        private async Task Instance_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            IMqttRouteHandler route = _mqttRoutes.Single(r => 
                                            r.Route.Equals(arg.ApplicationMessage.Topic, 
                                            StringComparison.InvariantCultureIgnoreCase));

            await route.Handle(Encoding.UTF8.GetString(arg.ApplicationMessage.PayloadSegment), 
                            arg.ApplicationMessage.ContentType, 
                            arg.ApplicationMessage.UserProperties.ToDictionary(k => k.Name, v => v.Value));
        }
    }
}