using EcleticaBeerControl.Domain.DomainEvents;

namespace EcleticaBeerControl.Domain.Entities.Base
{
    public abstract class Entity
    {
        private readonly List<DomainEvent> _domainEvents = new();
        protected Entity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public ICollection<DomainEvent> DomainEvents => _domainEvents;
        public string Id { get; set; }
        public required string CreateBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        protected void RaiseDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}