using EcleticaBeerControl.Infrastructure.Messaging.RabbitMq;
using EcleticaBeerControl.Worker.Managers;
using EcleticaBeerControl.Worker.Messaging.Consumers;
using RabbitMQ.Client.Core.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection.Configuration;

namespace EcleticaBeerControl.Worker
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorker(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.ConfigureRabbitMqTopology()
                    .ConfigureRabbitMqConsumeres();

            services.AddScoped<RealtimeClientManager>();
            services.AddScoped<MqttClientManager>();

            return services;
        }

        private static IServiceCollection ConfigureRabbitMqTopology(this IServiceCollection services) {
            services.AddConsumptionExchange("ebc.devices", RabbitMqConfiguration.Topology);
            return services;
        }

        private static IServiceCollection ConfigureRabbitMqConsumeres(this IServiceCollection services)
        {
            services.AddAsyncMessageHandlerSingleton<DeviceCreatedMessageHandler>("device-created");
            return services;
        }
    }
}
