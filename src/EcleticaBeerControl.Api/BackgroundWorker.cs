using ILogger = Serilog.ILogger;

namespace EcleticaBeerControl.Api
{
    public class BackgroundWorker : BackgroundService
    {
        private readonly ILogger _logger;

        public BackgroundWorker(ILogger logger) {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information("Executing background worker...");
            return Task.CompletedTask;
        }

    }
}