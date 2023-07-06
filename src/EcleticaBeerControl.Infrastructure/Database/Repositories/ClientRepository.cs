using EcleticaBeerControl.Application.Repositories;
using Client = EcleticaBeerControl.Application.Domain.Entities.Client;

namespace EcleticaBeerControl.Infrastructure.Database.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly Supabase.Client _client;

        public ClientRepository(Supabase.Client client)
        {
            _client = client;
        }

        public async Task<IList<Client>> GetAll()
        {
            var clients = await _client.From<Client>().Get();
            return clients.Models;

        }
    }
}
