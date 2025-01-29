using EcleticaBeerControl.Domain.DomainEvents.Devices;
using EcleticaBeerControl.Domain.Entities.Base;
using EcleticaBeerControl.Domain.Enums;

namespace EcleticaBeerControl.Domain.Entities
{
    public class Device : BreweryEntity
    {
        public string Name { get; init; } = string.Empty;
        public string Identifier { get; init; } = string.Empty;
        public string? Description { get; init; }
        public DeviceStatus Status { get; init; } = DeviceStatus.Connecting;

        public static Device Create(string breweryId, string identifier, string name, string? description, string createdBy)
        {
            var entity = new Device
            {
                BreweryId = breweryId,
                Name = name,
                Identifier = identifier,
                Description = description,
                Status = DeviceStatus.Connecting,
                CreateBy = createdBy
            };

            entity.RaiseDomainEvent(new DeviceCreatedEvent
            {
                Identifier = entity.Identifier,
                BreweryId = entity.BreweryId
            });

            return entity;
        }
    }
}
