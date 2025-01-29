using EcleticaBeerControl.Infrastructure.Mqtt;
using EcleticaBeerControl.Worker.Mqtt;
using Serilog;

namespace EcleticaBeerControl.Worker
{
    public class Worker : BackgroundService
    {
        private readonly MqttConnectionManager _mqttConnectionManager;
        private readonly IMqttMessageRouter _mqttMessageRouter;
        public Worker(MqttConnectionManager mqttConnectionManager, IMqttMessageRouter mqttMessageRouter)
        {
            _mqttConnectionManager = mqttConnectionManager;
            _mqttMessageRouter = mqttMessageRouter;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("IoT Communication initializing ...");

            await _mqttConnectionManager.Initialize(stoppingToken);
            await _mqttMessageRouter.Initialize(stoppingToken);

            Log.Information("IoT Communication initialized");
        }
    }
}