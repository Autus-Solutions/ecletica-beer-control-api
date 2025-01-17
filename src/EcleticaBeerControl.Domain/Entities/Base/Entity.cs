using EcleticaBeerControl.Domain.DomainEvents;

namespace EcleticaBeerControl.Domain.Entities.Base
{
    public abstract class Entity
    {
        private readonly List<DomainEvent> _domainEvents = new();
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public ICollection<DomainEvent> DomainEvents => _domainEvents;
        public Guid Id { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        protected void RaiseDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}