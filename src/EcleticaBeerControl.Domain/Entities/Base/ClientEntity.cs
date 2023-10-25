using Postgrest.Attributes;

namespace EcleticaBeerControl.Domain.Entities.Base
{
    public abstract class ClientEntity : Entity
    {
        protected ClientEntity() : base()
        { 
        }

        [Column("brewery_id")]
        public Guid BreweryId { get; set; }
    }
}