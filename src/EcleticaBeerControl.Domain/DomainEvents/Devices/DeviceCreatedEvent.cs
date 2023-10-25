namespace EcleticaBeerControl.Domain.DomainEvents.Devices
{
    public record DeviceCreatedEvent : DomainEvent
    {
        public required Guid BreweryId { get; set; }
        public required string Identifier { get; set; }
    };
}
