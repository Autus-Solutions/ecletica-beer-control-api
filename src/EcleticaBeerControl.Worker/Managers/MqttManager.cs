using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Formatter;

namespace EcleticaBeerControl.Worker.Managers
{
    public sealed class MqttManager
    {
        private readonly MqttClientOptions _mqttClientOptions;
        private readonly IMqttClient _mqttClient;
        public MqttManager()
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

            await _mqttClient.DisconnectAsync(MqttClientDisconnectOptionsReason.NormalDisconnection);
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
            var topicFilter = new MqttTopicFilterBuilder().WithTopic("ebc-01/analytics").Build();
            await _mqttClient.SubscribeAsync(topicFilter, CancellationToken.None);
        }
    }
}
