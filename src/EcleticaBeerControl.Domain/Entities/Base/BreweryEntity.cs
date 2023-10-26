using Postgrest.Attributes;

namespace EcleticaBeerControl.Domain.Entities.Base
{
    public abstract class BreweryEntity : Entity
    {
        [Column("brewery_id")]
        public Guid BreweryId { get; set; }
        public virtual Brewery Brewery { get; set; }
    }
}