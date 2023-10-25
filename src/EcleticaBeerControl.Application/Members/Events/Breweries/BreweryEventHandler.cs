using EcleticaBeerControl.Domain.DomainEvents.Devices;
using EcleticaBeerControl.Domain.Primitives;
using MediatR;
using Serilog;
using Supabase.Gotrue;

namespace EcleticaBeerControl.Application.Members.Events.Devices
{
    internal sealed class BreweryEventHandler
        : INotificationHandler<BreweryRegistredEvent>
    {
        private readonly Client _auth;
        private readonly BreweryUserContext _brewer;

        public BreweryEventHandler(Client auth, BreweryUserContext brewer)
        {
            _auth = auth;
            _brewer = brewer;
        }

        public async Task Handle(BreweryRegistredEvent notification, CancellationToken cancellationToken)
        {
            Log.Information("Updating current user metadata");

            await _auth.Update(new UserAttributes
            {
                Data = _brewer.ToUserMetadata()
            });
        }
    }
}