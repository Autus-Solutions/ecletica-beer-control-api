using EcleticaBeerControl.Worker.Managers;
using Serilog;

namespace EcleticaBeerControl.Worker
{
    public class Worker : BackgroundService
    {
        private readonly MqttClientManager _mqttManager;
        private readonly RealtimeClientManager _realtimeManager;

        public Worker(MqttClientManager mqttManager, RealtimeClientManager realtimeManager)
        {
            _mqttManager = mqttManager;
            _realtimeManager = realtimeManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("Worker Started !");

            await _realtimeManager.Instantiate(stoppingToken);
            await _mqttManager.Instantiate(stoppingToken);
        }
    }
}