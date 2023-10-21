using RabbitMQ.Client.Core.DependencyInjection.Configuration;


namespace EcleticaBeerControl.Infrastructure.Messaging.RabbitMq
{
    public static class RabbitMqConfiguration
    {
        public static RabbitMqExchangeOptions Topology => new RabbitMqExchangeOptions
        {
            Durable = true,
            Type = "direct",
            RequeueFailedMessages = true,
            DeadLetterExchange = "ebc.deadletter",
            Queues = new List<RabbitMqQueueOptions>
                {
                    new RabbitMqQueueOptions
                    {
                        Durable = true,
                        Name = "devices-created",
                        RoutingKeys = new HashSet<string>
                        {
                            "device-created"
                        }
                    }
                }
        };
    }
}
