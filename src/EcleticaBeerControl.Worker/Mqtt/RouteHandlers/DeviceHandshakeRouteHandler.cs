using EcleticaBeerControl.Infrastructure.Mqtt;
using EcleticaBeerControl.Worker.Payloads;
using Newtonsoft.Json;
using Serilog;

namespace EcleticaBeerControl.Worker.Mqtt.RoutesHandlers
{
    public sealed class DeviceHandshakeRouteHandler : IMqttRouteHandler
    {
        public string Route => RouteKeys.DeviceApplicationHandshakeResultRoute;


        public Task Handle(string payload, string contentType, IDictionary<string, string> brewerProperties)
        {
            var convertedPayload = JsonConvert.DeserializeObject<DeviceTemperatureMetric>(payload);
            Log.Information($"Temperatura do dispositivo `{brewerProperties["device_identifier"]}` atualizada: {convertedPayload!.Degrees}");
            return Task.CompletedTask;
        }
    }
}
