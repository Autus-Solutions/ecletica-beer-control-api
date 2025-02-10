using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Core.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Templates.Themes;
using SerilogTracing.Expressions;

namespace EcleticaBeerControl.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSerilog(configuration)
                    .ConfigureRabbitMq(configuration)
                    .ConfigurHealthChecks(configuration);

            return services;
        }

        private static IServiceCollection ConfigureSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            const string ApplicationName = "Ecletica Beer Control";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
                .Enrich.WithProperty("Application", ApplicationName)
                .WriteTo.Console(Formatters.CreateConsoleTextFormatter(theme: TemplateTheme.Code))
                .CreateLogger();

            Log.Information($"{ApplicationName} Starting...");

            return services;
        }

        private static IServiceCollection ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRabbitMqServices(new RabbitMqServiceOptions
            {
                HostName = configuration["MessageBroker:Host"]!,
                Port = int.Parse(configuration["MessageBroker:Port"]!),
                VirtualHost = configuration["MessageBroker:VHost"]!,
                UserName = configuration["MessageBroker:Username"]!,
                Password = configuration["MessageBroker:Password"]!
            });

            return services;
        }

        private static IServiceCollection ConfigurHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConnectionString = configuration.GetConnectionString("Database");
            var amqpConnectionString = configuration.GetConnectionString("Amqp");

            services.AddHealthChecks()
                    .AddNpgSql(databaseConnectionString!);

            return services;
        }
    }
}
