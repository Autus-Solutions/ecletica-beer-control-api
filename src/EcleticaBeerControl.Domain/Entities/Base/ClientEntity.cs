using Postgrest.Attributes;

namespace EcleticaBeerControl.Domain.Entities.Base
{
    public abstract class ClientEntity : Entity
    {
        protected ClientEntity() : base()
        { 
        }

        [Column("client_id")]
        public Guid ClientId { get; set; }
    }
}