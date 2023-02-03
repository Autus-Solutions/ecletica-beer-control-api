using NLog;

namespace EcleticaBeerMonitor.Worker
{
    public class Worker : BackgroundService
    {
        private readonly Logger _logger;
        private readonly IConfiguration _configuration;

        public Worker(Logger logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.Info("Its working!");
            }
        }

    }
}