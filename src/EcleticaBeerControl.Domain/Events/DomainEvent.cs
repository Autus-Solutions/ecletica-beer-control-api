using MediatR;

namespace EcleticaBeerControl.Domain.Events
{
    public abstract record DomainEvent(Guid Id) : INotification;
}
