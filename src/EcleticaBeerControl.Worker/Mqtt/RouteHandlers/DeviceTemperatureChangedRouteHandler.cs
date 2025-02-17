using EcleticaBeerControl.Infrastructure.Mqtt;
using EcleticaBeerControl.Persistence.EF.Database;
using EcleticaBeerControl.Worker.Adapters;
using EcleticaBeerControl.Worker.Payloads;
using Newtonsoft.Json;
using Serilog;

namespace EcleticaBeerControl.Worker.Mqtt.RoutesHandlers
{
    public sealed class DeviceTemperatureChangedRouteHandler(TimeseriesDbContext timeseriesDbContext) : IMqttRouteHandler
    {
        private readonly TimeseriesDbContext _timeseriesDbContext = timeseriesDbContext;

        public string Route => RouteKeys.DeviceApplicationTemepratureChangedRoute;

        public async Task Handle(string payload, string contentType, IDictionary<string, string> brewerProperties)
        {
            var temperature = JsonConvert.DeserializeObject<Temperature>(payload);
            var deviceIdentifier = brewerProperties["device_identifier"];

            Log.Information($"Temperatura do dispositivo `{deviceIdentifier}` atualizada: {temperature!.Degrees}");

            _timeseriesDbContext.Add(TemperatureAdapter.Adapt(deviceIdentifier, temperature));

            await _timeseriesDbContext.SaveChangesAsync();
        }
    }
}
