using EcleticaBeerControl.Domain.DomainEvents.Devices;
using MediatR;
using Serilog;

namespace EcleticaBeerControl.Application.Members.Events.Devices
{
    internal sealed class DeviceNotificationHandler 
        : INotificationHandler<DeviceCreatedEvent>
    {
        private readonly ILogger _logger;

        public DeviceNotificationHandler(ILogger logger)
        {
            _logger = logger;
        }
        public Task Handle(DeviceCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.Information("Publishing {EventName}, {EventId}", nameof(DeviceCreatedEvent), notification.Id);
            return Task.CompletedTask;
        }
    }
}