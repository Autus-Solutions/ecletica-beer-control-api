namespace EcleticaBeerControl.Domain.DomainEvents.Devices
{
    public record DeviceCreatedEvent : DomainEvent
    {
        public required string BreweryId { get; set; }
        public required string Identifier { get; set; }
    };
}
