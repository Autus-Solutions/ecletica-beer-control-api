using EcleticaBeerControl.Infrastructure.Mqtt;
using Serilog;

namespace EcleticaBeerControl.Worker.Mqtt.RoutesHandlers
{
    public sealed class DeviceTemperatureChangedRouteHandler : IMqttRouteHandler
    {
        public string Route => RouteKeys.DeviceTemepratureChangedRoute;

        public Task Handle(string payload, string contentType, IDictionary<string, string> userProperties)
        {
            Log.Information(@"Message arrived: Route => {Route}, 
                                Payload => {Payload}, 
                                ContentType => {ContentType}
                                UserProperties => {UserProperties}",
                                Route,
                                payload,
                                contentType,
                                userProperties);

            return Task.CompletedTask;
        }
    }
}
