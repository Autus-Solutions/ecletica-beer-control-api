using EcleticaBeerControl.Worker.Managers;
using MQTTnet.Client;
using System.Text;
using ILogger = Serilog.ILogger;

namespace EcleticaBeerControl.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly MqttManager _mqttManager;
        private readonly RealtimeManager _realtimeManager;

        public Worker(ILogger logger, MqttManager mqttManager, RealtimeManager realtimeManager)
        {
            _logger = logger;
            _mqttManager = mqttManager;
            _realtimeManager = realtimeManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information("Executing background worker...");

            await _realtimeManager.Instantiate(stoppingToken);
            await _mqttManager.Instantiate(stoppingToken);

            _mqttManager.Instance.ApplicationMessageReceivedAsync += Instance_ApplicationMessageReceivedAsync;
        }

        private Task Instance_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
            Console.WriteLine($"+ Topic = {arg.ApplicationMessage.Topic}");
            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)}");
            Console.WriteLine($"+ QoS = {arg.ApplicationMessage.QualityOfServiceLevel}");
            Console.WriteLine($"+ Retain = {arg.ApplicationMessage.Retain}");
            Console.WriteLine();

            return Task.CompletedTask;
        }
    }
}