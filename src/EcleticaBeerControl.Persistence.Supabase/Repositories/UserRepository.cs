using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Repositories;
using Supabase;

namespace EcleticaBeerControl.Persistence.Supabase.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Client _client;

        public UserRepository(Client client)
        {
            _client = client;
        }

        public async Task Insert(User entity, CancellationToken cancellationToken = default)
        {
            await _client.From<User>().Insert(entity, cancellationToken: cancellationToken);
        }
    }
}
