using MediatR;

namespace EcleticaBeerControl.Domain.Events
{
    public abstract record DomainEvent : INotification
    {
        public Guid Id { get; set; }
    };
}
