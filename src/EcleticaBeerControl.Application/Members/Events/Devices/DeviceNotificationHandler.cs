using EcleticaBeerControl.Domain.DomainEvents.Devices;
using MassTransit;
using MediatR;
using Serilog;

namespace EcleticaBeerControl.Application.Members.Events.Devices
{
    internal sealed class DeviceNotificationHandler 
        : INotificationHandler<DeviceCreatedEvent>
    {
        private readonly ILogger _logger;
        private readonly IPublishEndpoint _publisher;

        public DeviceNotificationHandler(ILogger logger, IPublishEndpoint publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        public Task Handle(DeviceCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.Information("Publishing {EventName}, {EventId}", nameof(DeviceCreatedEvent), notification.Id);

            _publisher.Publish(notification, cancellationToken);

            return Task.CompletedTask;
        }
    }
}