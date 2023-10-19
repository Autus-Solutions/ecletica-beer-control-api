using MediatR;

namespace EcleticaBeerControl.Domain.Events
{
    public record DomainEvent(Guid Id) : INotification;
}
