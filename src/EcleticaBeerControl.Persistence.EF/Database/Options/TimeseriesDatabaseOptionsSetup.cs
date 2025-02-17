using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EcleticaBeerControl.Persistence.EF.Database.Options
{
    internal sealed class TimeseriesDatabaseOptionsSetup : IConfigureOptions<TimeseriesDatabaseOptions>
    {
        private const string ConfigurationSessionName = "TimeseriesDatabaseOptions";
        private readonly IConfiguration _configuration;

        public TimeseriesDatabaseOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(TimeseriesDatabaseOptions options)
        {
            options.ConnectionString = _configuration.GetConnectionString("Timeseries")!;
            _configuration.GetSection(ConfigurationSessionName).Bind(options);
        }
    }
}
