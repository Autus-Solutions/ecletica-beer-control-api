using EcleticaBeerControl.Domain.DomainEvents;
using Newtonsoft.Json;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EcleticaBeerControl.Domain.Entities.Base
{
    public abstract class Entity : BaseModel
    {
        private readonly List<DomainEvent> _domainEvents = new();
        protected Entity()
        {
            Id = Guid.NewGuid();    
        }

        [JsonIgnore]
        public ICollection<DomainEvent> DomainEvents => _domainEvents;

        [PrimaryKey("id", true)]
        public Guid Id { get; set; }

        [Column("created_by")]
        public Guid CreateBy { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_by")]
        public Guid? UpdatedBy { get; set; }

        [Column("created_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("deleted_by")]
        public Guid? DeletedBy { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }


        protected void RaiseDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}