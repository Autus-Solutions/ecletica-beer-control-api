using EcleticaBeerControl.Domain.Events;

namespace EcleticaBeerControl.Domain.DomainEvents.Devices
{
    public record DeviceCreatedEvent : DomainEvent
    {
        public required Guid ClientId { get; set; }
        public required string Identifier { get; set; }
    };
}
