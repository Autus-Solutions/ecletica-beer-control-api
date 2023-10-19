using EcleticaBeerControl.Domain.Entities.Base;
using MediatR;

namespace EcleticaBeerControl.Application.Members.Commands
{
    internal abstract class BaseCommandHandler
    {
        protected readonly IPublisher _publisher; 
        protected BaseCommandHandler(IPublisher publisher) {
            _publisher = publisher;
        }

        protected async Task PublishEvents(Entity entity, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in entity.DomainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }
        }
    }
}
