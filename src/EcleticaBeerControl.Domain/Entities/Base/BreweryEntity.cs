using EcleticaBeerControl.Domain.Interfaces;

namespace EcleticaBeerControl.Domain.Entities.Base
{
    public abstract class BreweryEntity : Entity, IBreweryEntity
    {
        public required string BreweryId { get; set; }
        public virtual Brewery? Brewery { get; set; }
    }
}