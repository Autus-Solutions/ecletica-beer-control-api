using EcleticaBeerControl.Domain.DomainEvents.Devices;
using MediatR;
using Serilog;

namespace EcleticaBeerControl.Application.Members.Events.Devices
{
    internal sealed class AuthEventHandler
        : INotificationHandler<BreweryRegistredEvent>
    {
        private readonly Domain.Models.User _brewer;

        public AuthEventHandler(Domain.Models.User brewer)
        {
            _brewer = brewer;
        }

        public async Task Handle(BreweryRegistredEvent notification, CancellationToken cancellationToken)
        {
            Log.Information("Updating current user metadata ...");
        }
    }
}