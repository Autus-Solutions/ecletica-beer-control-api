using EcleticaBeerControl.Domain.DomainEvents.Devices;
using EcleticaBeerControl.Domain.Entities.Base;
using EcleticaBeerControl.Domain.Enums;
using Postgrest.Attributes;

namespace EcleticaBeerControl.Domain.Entities
{
    [Table("devices")]
    public class Device : BreweryEntity
    {
        [Column("name")]
        public string Name { get; init; } = string.Empty;

        [Column("identifier")]
        public string Identifier { get; init; } = string.Empty;

        [Column("description")]
        public string? Description { get; init; }

        [Column("status")]
        public DeviceStatus Status { get; init; } = DeviceStatus.Connecting;

        public static Device Create(Guid breweryId, string identifier, string name, string? description, Guid createdBy)
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
