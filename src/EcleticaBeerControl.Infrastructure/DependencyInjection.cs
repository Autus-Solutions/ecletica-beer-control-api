using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection.Configuration;

namespace EcleticaBeerControl.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureRabbitMq(configuration);

            return services;
        }

        private static IServiceCollection ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRabbitMqServices(new RabbitMqServiceOptions
            {
                HostName = configuration["MessageBroker:Host"]!,
                Port = int.Parse(configuration["MessageBroker:Port"]!),
                VirtualHost = configuration["MessageBroker:VHost"]!,
                UserName = configuration["MessageBroker:UserName"]!,
                Password = configuration["MessageBroker:Password"]!
            });

            return services;
        }
    }
}
