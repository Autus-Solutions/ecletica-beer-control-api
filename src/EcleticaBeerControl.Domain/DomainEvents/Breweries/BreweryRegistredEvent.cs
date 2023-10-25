using EcleticaBeerControl.Domain.Events;

namespace EcleticaBeerControl.Domain.DomainEvents.Devices
{
    public record BreweryRegistredEvent : DomainEvent
    {
        public required Guid UserId { get; set; }
    };
}
