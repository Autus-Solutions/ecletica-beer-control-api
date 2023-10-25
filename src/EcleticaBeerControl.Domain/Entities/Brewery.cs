using EcleticaBeerControl.Domain.DomainEvents.Devices;
using EcleticaBeerControl.Domain.Entities.Base;
using Postgrest.Attributes;

namespace EcleticaBeerControl.Domain.Entities
{
    [Table("breweries")]
    public class Brewery : Entity
    {
        [Column("logo")]
        public string? Logo { get; init; }
        [Column("name")]
        public string Name { get; init; }

        public static Brewery Register(Guid id, string name, Guid createdBy)
        {
            var brewery = new Brewery
            {
                Id = id,
                Name = name,
                CreateBy = createdBy,
                CreatedAt = DateTime.UtcNow
            };

            return brewery;
        }
    }
}
