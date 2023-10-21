using EcleticaBeerControl.Infrastructure.Mqtt;
using EcleticaBeerControl.Worker.Mqtt;
using EcleticaBeerControl.Worker.Realtime;
using Serilog;

namespace EcleticaBeerControl.Worker
{
    public class Worker : BackgroundService
    {
        private readonly MqttConnectionManager _mqttConnectionManager;
        private readonly RealtimeConnectionManager _realtimeConnectionManager;
        private readonly IMqttMessageRouter _mqttMessageRouter;
        public Worker(MqttConnectionManager mqttConnectionManager, RealtimeConnectionManager realtimeConnectionManager, IMqttMessageRouter mqttMessageRouter)
        {
            _mqttConnectionManager = mqttConnectionManager;
            _realtimeConnectionManager = realtimeConnectionManager;
            _mqttMessageRouter = mqttMessageRouter;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("IoT Communication initializing ...");

            await _realtimeConnectionManager.Initialize(stoppingToken);
            await _mqttConnectionManager.Initialize(stoppingToken);
            await _mqttMessageRouter.Initialize(stoppingToken);

            Log.Information("IoT Communication initialized");
        }
    }
}