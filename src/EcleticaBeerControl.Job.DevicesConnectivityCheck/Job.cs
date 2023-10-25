namespace EcleticaBeerControl.DevicesConnectivityCheck
{
    public class Job : BackgroundService
    {
        private readonly ILogger<Job> _logger;

        public Job(ILogger<Job> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}