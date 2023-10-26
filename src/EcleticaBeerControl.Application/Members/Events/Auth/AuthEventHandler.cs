using EcleticaBeerControl.Domain.Contexts;
using EcleticaBeerControl.Domain.DomainEvents.Devices;
using MediatR;
using Serilog;
using Supabase.Gotrue;

namespace EcleticaBeerControl.Application.Members.Events.Devices
{
    internal sealed class AuthEventHandler
        : INotificationHandler<BreweryRegistredEvent>
    {
        private readonly Client _authClient;
        private readonly BreweryUserContext _brewer;

        public AuthEventHandler(Client authClient, BreweryUserContext brewer)
        {
            _authClient = authClient;
            _brewer = brewer;
        }

        public async Task Handle(BreweryRegistredEvent notification, CancellationToken cancellationToken)
        {
            Log.Information("Updating current user metadata ...");

            await _authClient.Update(new UserAttributes
            {
                Data = _brewer.ToUserMetadata()
            });
        }
    }
}