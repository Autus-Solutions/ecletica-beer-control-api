using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Repositories;
using Supabase;

namespace EcleticaBeerControl.Persistence.Supabase.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly Client _client;

        public DeviceRepository(Client client)
        {
            _client = client;
        }

        public async Task Insert(Device entity, CancellationToken cancellationToken = default)
        {
            await _client.From<Device>().Insert(entity, cancellationToken: cancellationToken);
        }
    }
}
