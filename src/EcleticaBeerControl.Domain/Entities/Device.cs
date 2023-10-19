using EcleticaBeerControl.Domain.DomainEvents.Devices;
using EcleticaBeerControl.Domain.Entities.Base;
using EcleticaBeerControl.Domain.Enums;
using Postgrest.Attributes;

namespace EcleticaBeerControl.Domain.Entities
{
    [Table("devices")]
    public class Device : ClientEntity
    {
        [Column("name")]
        public string Name { get; init; }

        [Column("description")]
        public string Description { get; init; }

        [Column("status")]
        public DeviceStatus Status { get; init; } = DeviceStatus.Connecting;

        public static Device Create(Guid clientId, string name, string description, Guid createdBy)
        {
            var device = new Device
            {
                ClientId = clientId,
                Name = name,
                Description = description,
                Status = DeviceStatus.Connecting,
                CreateBy = createdBy,
                CreatedAt = DateTime.UtcNow
            };

            device.RaiseDomainEvent(new DeviceCreatedEvent(device.Id));

            return device;
        }
    }
}
