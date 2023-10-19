using EcleticaBeerControl.Domain.Events;
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

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}