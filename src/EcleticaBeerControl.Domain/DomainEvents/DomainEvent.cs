using MediatR;

namespace EcleticaBeerControl.Domain.DomainEvents
{
    public abstract record DomainEvent : INotification
    {
    };
}
