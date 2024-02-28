using EcleticaBeerControl.Domain.Entities;
using Serilog;
using Supabase;
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
            Log.Information("Supabase Realtime initializing ...");

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

            Log.Information("Supabase Realtime initialized");
        }
    }
}
