using EcleticaBeerControl.Domain.Entities;
using Supabase.Realtime;
using System.Diagnostics;
using static Supabase.Realtime.PostgresChanges.PostgresChangesOptions;

namespace EcleticaBeerControl.Worker.Realtime
{
    public sealed class RealtimeConnectionManager
    {
        private readonly Client _client;

        public RealtimeConnectionManager(Client client)
        {
            _client = client;
        }

        public async Task Initialize(CancellationToken cancellationToken = default)
        {
            var client = await _client.ConnectAsync();
            var channel = client.Channel("realtime", "public", "devices");

            channel.AddPostgresChangeHandler(ListenType.Inserts, (sender, change) =>
            {
                // The event type
                Debug.WriteLine(change.Event);
                // The changed record
                Debug.WriteLine(change.Model<Device>());
            });

            await channel.Subscribe();
        }
    }
}
