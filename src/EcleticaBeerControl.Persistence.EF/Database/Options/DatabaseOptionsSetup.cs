using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EcleticaBeerControl.Persistence.EF.Database.Options
{
    internal sealed class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
    {
        private const string ConfigurationSessionName = "DatabaseOptions";
        private readonly IConfiguration _configuration;

        public DatabaseOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(DatabaseOptions options)
        {
            options.ConnectionString = _configuration.GetConnectionString("Database")!;
            _configuration.GetSection(ConfigurationSessionName).Bind(options);
        }
    }
}
