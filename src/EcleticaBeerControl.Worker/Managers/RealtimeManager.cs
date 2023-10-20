using Supabase;
using System.Diagnostics;
using static Supabase.Realtime.PostgresChanges.PostgresChangesOptions;

namespace EcleticaBeerControl.Worker.Managers
{
    public class RealtimeManager
    {
        private readonly Client _client;

        public RealtimeManager(Client client)
        {
            _client = client;
        }

        public async Task Instantiate(CancellationToken cancellationToken = default)
        {
            var client = await _client.InitializeAsync();
            var realtimeClient = await client.Realtime.ConnectAsync();
            var channel = realtimeClient.Channel("realtime", "public", "devices");

            channel.AddPostgresChangeHandler(ListenType.All, (sender, change) =>
            {
                // The event type
                Debug.WriteLine(change.Event);
                // The changed record
                Debug.WriteLine(change.Payload);
            });

            await channel.Subscribe();
        }
    }
}
