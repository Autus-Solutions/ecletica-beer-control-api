using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Repositories;

namespace EcleticaBeerControl.Infrastructure.Database.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly Supabase.Client _client;

        public DeviceRepository(Supabase.Client client)
        {
            _client = client;
        }

        public async Task Insert(Device entity, CancellationToken cancellationToken = default)
        {
            await _client.From<Device>().Insert(entity, cancellationToken: cancellationToken);
        }
    }
}
