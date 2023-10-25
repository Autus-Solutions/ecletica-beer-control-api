using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Repositories;
using Supabase;

namespace EcleticaBeerControl.Persistence.Supabase.Repositories
{
    public class BreweryRepository : IBreweryRepository
    {
        private readonly Client _client;

        public BreweryRepository(Client client)
        {
            _client = client;
        }

        public async Task Insert(Brewery entity, CancellationToken cancellationToken = default)
        {
            await _client.From<Brewery>().Insert(entity, cancellationToken: cancellationToken);
        }
    }
}
