using EcleticaBeerControl.Domain.Entities;
using Supabase;
using System.Diagnostics;
using static Supabase.Realtime.PostgresChanges.PostgresChangesOptions;

namespace EcleticaBeerControl.Worker.Managers
{
    public sealed class RealtimeClientManager
    {
        private readonly Client _client;

        public RealtimeClientManager(Client client)
        {
            _client = client;
        }

        public async Task Instantiate(CancellationToken cancellationToken = default)
        {
            var client = await _client.InitializeAsync();
            var realtimeClient = await client.Realtime.ConnectAsync();
            var channel = realtimeClient.Channel("realtime", "public", "devices");

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
