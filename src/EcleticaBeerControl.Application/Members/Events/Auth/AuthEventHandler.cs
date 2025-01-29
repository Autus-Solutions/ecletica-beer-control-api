using EcleticaBeerControl.Domain.DomainEvents.Devices;
using MediatR;
using Serilog;

namespace EcleticaBeerControl.Application.Members.Events.Devices
{
    internal sealed class AuthEventHandler
        : INotificationHandler<BreweryRegistredEvent>
    {
        public AuthEventHandler()
        {

        }

        public async Task Handle(BreweryRegistredEvent notification, CancellationToken cancellationToken)
        {
            Log.Information("Updating current user metadata ...");
        }
    }
}