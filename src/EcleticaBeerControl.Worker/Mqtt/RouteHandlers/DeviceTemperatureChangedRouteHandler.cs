using EcleticaBeerControl.Infrastructure.Mqtt;
using EcleticaBeerControl.Worker.Adapters;
using EcleticaBeerControl.Worker.Payloads;
using Newtonsoft.Json;
using Serilog;

namespace EcleticaBeerControl.Worker.Mqtt.RoutesHandlers
{
    public sealed class DeviceTemperatureChangedRouteHandler() : IMqttRouteHandler
    {
        public string Route => RouteKeys.DeviceApplicationTemepratureChangedRoute;

        public async Task Handle(string payload, string contentType, IDictionary<string, string> brewerProperties)
        {
            var temperature = JsonConvert.DeserializeObject<Temperature>(payload);
            var deviceIdentifier = brewerProperties["device_identifier"];
            var temperatureEntity = TemperatureAdapter.Adapt(deviceIdentifier, temperature!);

            Log.Information($"Temperatura do dispositivo `{deviceIdentifier}` atualizada: {temperatureEntity!.Value}");
        }
    }
}
