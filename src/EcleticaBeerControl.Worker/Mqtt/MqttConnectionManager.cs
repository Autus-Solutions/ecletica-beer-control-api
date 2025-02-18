using MQTTnet.Client;

namespace EcleticaBeerControl.Worker.Mqtt
{
    public sealed class MqttConnectionManager
    {
        private readonly MqttClientOptions _mqttClientOptions;
        private readonly IMqttClient _mqttClient;

        public MqttConnectionManager(MqttClientOptions mqttClientOptions, IMqttClient mqttClient)
        {
            _mqttClientOptions = mqttClientOptions;
            _mqttClient = mqttClient;
        }

        public async Task Initialize(CancellationToken cancellationToken = default)
        {
            _mqttClient.DisconnectedAsync += Instance_DisconnectedAsync;
            await _mqttClient.ConnectAsync(_mqttClientOptions, cancellationToken);
        }

        private async Task Instance_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));

            try
            {
                await _mqttClient.ReconnectAsync(CancellationToken.None);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
