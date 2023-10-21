using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Formatter;
using System.Text;

namespace EcleticaBeerControl.Worker.Managers
{
    public sealed class MqttClientManager
    {
        private readonly MqttClientOptions _mqttClientOptions;
        private readonly IMqttClient _mqttClient;
        public MqttClientManager()
        {
            _mqttClientOptions = new MqttClientOptionsBuilder()
                .WithClientId("ebc-api")
                .WithTcpServer("mqtt.ecletica.beer", 30000)
                .WithProtocolVersion(MqttProtocolVersion.V500)
                .WithCleanSession()
                .Build();

            _mqttClient = new MqttFactory().CreateMqttClient();
        }

        public async Task Instantiate(CancellationToken cancellationToken = default)
        {
            _mqttClient.DisconnectedAsync += Instance_DisconnectedAsync;
            _mqttClient.ConnectedAsync += Instance_ConnectedAsync;
            _mqttClient.ApplicationMessageReceivedAsync += Instance_ApplicationMessageReceivedAsync;

            await _mqttClient.ConnectAsync(_mqttClientOptions, cancellationToken);
        }

        public IMqttClient Instance => _mqttClient;

        private async Task Instance_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));

            try
            {
                await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
            }
            catch
            {

            }
        }

        private async Task Instance_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            var topicFilter = new MqttTopicFilterBuilder()
                .WithTopic("device/temperature-changed")
                .WithAtLeastOnceQoS()
                .Build();

            await _mqttClient.SubscribeAsync(topicFilter, CancellationToken.None);
        }

        private Task Instance_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
            Console.WriteLine($"+ Topic = {arg.ApplicationMessage.Topic}");
            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(arg.ApplicationMessage.PayloadSegment)}");
            Console.WriteLine($"+ QoS = {arg.ApplicationMessage.QualityOfServiceLevel}");
            Console.WriteLine($"+ Retain = {arg.ApplicationMessage.Retain}");
            Console.WriteLine($"+ Content Type = {arg.ApplicationMessage.ContentType}");

            arg.ApplicationMessage.UserProperties.ForEach(p => {
                Console.WriteLine($"+ User Property: {p.Name} = {p.Value}");
            });

            Console.WriteLine();

            return Task.CompletedTask;
        }
    }
}
