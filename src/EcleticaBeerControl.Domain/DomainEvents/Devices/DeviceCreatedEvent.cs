using EcleticaBeerControl.Domain.Events;

namespace EcleticaBeerControl.Domain.DomainEvents.Devices
{
    public record DeviceCreatedEvent(Guid Id) : DomainEvent(Id);
}
