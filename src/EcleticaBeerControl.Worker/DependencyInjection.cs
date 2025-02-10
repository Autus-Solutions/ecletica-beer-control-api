using EcleticaBeerControl.Infrastructure.Messaging.RabbitMq;
using EcleticaBeerControl.Infrastructure.Mqtt;
using EcleticaBeerControl.Worker.Messaging.Handlers;
using EcleticaBeerControl.Worker.Mqtt;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;
using RabbitMQ.Client.Core.DependencyInjection;

namespace EcleticaBeerControl.Worker
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorker(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.ConfigureRabbitMqTopology()
                    .ConfigureRabbitMqConsumeres()
                    .ConfigureMqtt(configuration);

            return services;
        }

        private static IServiceCollection ConfigureMqtt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<MqttClientOptions>((provider) =>
                 new MqttClientOptionsBuilder()
                .WithClientId("ecletica-beer-control-api")
                .WithTcpServer(configuration["MqttBroker:Host"]!, int.Parse(configuration["MqttBroker:Port"]!))
                .WithProtocolVersion(MqttProtocolVersion.V500)
                .WithCredentials(configuration["MqttBroker:Username"]!, configuration["MqttBroker:Password"]!)
                .WithCleanSession()
                .WithCleanStart()
                .Build()
            );

            services.AddSingleton<IMqttClient>((provider) => new MqttFactory().CreateMqttClient());
            services.AddSingleton<MqttConnectionManager>();

            #region MqttMessageRouter and Handlers

            services.AddScoped<IMqttMessageRouter, MqttMessageRouter>();

            services.Scan(scan => scan
                  .FromAssemblyOf<MqttMessageRouter>()
                      .AddClasses(classes => classes.Where(type => type.FullName!.EndsWith("RouteHandler")))
                      .AsImplementedInterfaces()
                      .AsSelf()
                      .WithScopedLifetime());

            #endregion

            return services;
        }

        private static IServiceCollection ConfigureRabbitMqTopology(this IServiceCollection services)
        {
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
