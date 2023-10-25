using EcleticaBeerControl.Domain.DomainEvents.Devices;
using EcleticaBeerControl.Domain.Primitives;
using MediatR;
using Serilog;
using Supabase;

namespace EcleticaBeerControl.Application.Members.Events.Devices
{
    internal sealed class BreweryEventHandler 
        : INotificationHandler<BreweryRegistredEvent>
    {
        private readonly Client _supabase;
        private readonly BreweryUser _brewer;

        public BreweryEventHandler(Client supabase, BreweryUser brewer)
        {
            _supabase = supabase;
            _brewer = brewer;
        }

        public async Task Handle(BreweryRegistredEvent notification, CancellationToken cancellationToken)
        {
            Log.Information("Updating current user metadata");

            await _supabase.Auth.Update(new Supabase.Gotrue.UserAttributes
            {
                Data = _brewer.ToUserMetadata()
            });
        }
    }
}