namespace EcleticaBeerControl.Domain.Entities.Base
{
    public abstract class BreweryEntity : Entity
    {
        public Guid BreweryId { get; set; }
        public virtual Brewery Brewery { get; set; }
    }
}