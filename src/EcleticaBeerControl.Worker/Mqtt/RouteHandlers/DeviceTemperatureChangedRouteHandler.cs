using EcleticaBeerControl.Infrastructure.Mqtt;
using EcleticaBeerControl.Persistence.EF.Database;
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
            var convertedPayload = JsonConvert.DeserializeObject<Temperature>(payload);
            Log.Information($"Temperatura do dispositivo `{brewerProperties["device_identifier"]}` atualizada: {convertedPayload!.Degrees}");

            _timeseriesDbContext.Add(new Temperature
            {
                Degrees = convertedPayload.Degrees,
                DegreeFormat = convertedPayload.DegreeFormat,
            });

            await _timeseriesDbContext.SaveChangesAsync();
        }
    }
}
