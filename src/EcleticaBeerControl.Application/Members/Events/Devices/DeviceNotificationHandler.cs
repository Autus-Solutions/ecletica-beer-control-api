using EcleticaBeerControl.Domain.DomainEvents.Devices;
using MediatR;
using RabbitMQ.Client.Core.DependencyInjection.Services.Interfaces;
using Serilog;

namespace EcleticaBeerControl.Application.Members.Events.Devices
{
    internal sealed class DeviceNotificationHandler 
        : INotificationHandler<DeviceCreatedEvent>
    {
        private readonly IProducingService _producingService;

        public DeviceNotificationHandler(IProducingService producingService)
        {
            _producingService = producingService;
        }

        public async Task Handle(DeviceCreatedEvent notification, CancellationToken cancellationToken)
        {
            Log.Information("Publishing {EventName}, {EventId}", nameof(DeviceCreatedEvent), notification.Id);
            await _producingService.SendAsync(notification, "ebc.devices", "device-created");
        }
    }
}