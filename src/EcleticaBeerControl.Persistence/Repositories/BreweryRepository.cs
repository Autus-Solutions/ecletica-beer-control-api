using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Repositories;


namespace EcleticaBeerControl.Persistence.Repositories
{
    public class BreweryRepository : IBreweryRepository
    {
        private readonly Supabase.Client _client;

        public BreweryRepository(Supabase.Client client)
        {
            _client = client;
        }

        public async Task Insert(Brewery entity, CancellationToken cancellationToken = default)
        {
            await _client.From<Brewery>().Insert(entity, cancellationToken: cancellationToken);
        }
    }
}
