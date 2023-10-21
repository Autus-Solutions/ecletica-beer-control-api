using MediatR;

namespace EcleticaBeerControl.Domain.Events
{
    public abstract record DomainEvent : INotification
    {
        public required Guid Id { get; set; }
    };
}
