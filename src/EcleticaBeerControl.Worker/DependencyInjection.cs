using EcleticaBeerControl.Domain.DomainEvents.Devices;
using EcleticaBeerControl.Domain.Events;
using EcleticaBeerControl.Worker.Managers;
using MassTransit;
using MassTransit.RabbitMqTransport.Topology;
using MediatR;
using RabbitMQ.Client;
using System.Security.Authentication;

namespace EcleticaBeerControl.Worker
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorker(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMassTransit((cfg) =>
            {
                cfg.SetKebabCaseEndpointNameFormatter();

                cfg.UsingRabbitMq((context, rabbitCfg) =>
                {
                    rabbitCfg.ReceiveEndpoint("device-created", e =>
                    {
                        e.Bind("ebc-devices", x =>
                        {
                            x.Durable = false;
                            x.AutoDelete = true;
                            x.ExchangeType = ExchangeType.Direct;
                            x.RoutingKey = nameof(DeviceCreatedEvent).ToLower();
                        });

                        e.Bind<DeviceCreatedEvent>();
                    });

                    rabbitCfg.Host(configuration["MessageBroker:Host"], 5671, configuration["MessageBroker:Username"], host =>
                    {
                        host.Username(configuration["MessageBroker:Username"]);
                        host.Password(configuration["MessageBroker:Password"]);
                        host.UseSsl(s =>
                        {
                            s.Protocol = SslProtocols.Tls12;
                        });
                    });

                    rabbitCfg.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<RealtimeManager>();
            services.AddScoped<MqttManager>();

            return services;
        }
    }
}
