using EcleticaBeerControl.Infrastructure.Messaging.RabbitMq;
using EcleticaBeerControl.Infrastructure.Mqtt;
using EcleticaBeerControl.Worker.Messaging.Handlers;
using EcleticaBeerControl.Worker.Mqtt;
using EcleticaBeerControl.Worker.Realtime;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;
using RabbitMQ.Client.Core.DependencyInjection;
using Supabase;

namespace EcleticaBeerControl.Worker
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorker(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.ConfigureRabbitMqTopology()
                    .ConfigureRabbitMqConsumeres();

            services.AddSupabaseRealtime(configuration);

            services.AddMqtt(configuration);

            return services;
        }

        private static IServiceCollection AddSupabaseRealtime(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped((provider) => new Client(
                configuration["SupabaseProjectUrl"] ?? string.Empty,
                configuration["SupabaseProjectSecretKey"] ?? string.Empty)
            );
            services.AddScoped<RealtimeConnectionManager>();
            return services;
        }

        private static IServiceCollection AddMqtt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<MqttClientOptions>((provider) =>
                 new MqttClientOptionsBuilder()
                .WithClientId("ebc-api")
                .WithTcpServer("mqtt.ecletica.beer", 30000)
                .WithProtocolVersion(MqttProtocolVersion.V500)
                .WithCleanSession()
                .WithCleanStart()
                .Build()
            ); ;

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
