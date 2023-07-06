using Postgrest.Attributes;

namespace EcleticaBeerControl.Application.Domain.Entities.Base
{
    public abstract class BaseClientEntity : BaseEntity
    {
        [Column("client_id")]
        public Guid ClientId { get; set; }
    }
}