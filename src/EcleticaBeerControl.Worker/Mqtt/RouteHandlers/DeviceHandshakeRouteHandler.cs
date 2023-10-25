using EcleticaBeerControl.Infrastructure.Mqtt;
using Serilog;

namespace EcleticaBeerControl.Worker.Mqtt.RoutesHandlers
{
    public sealed class DeviceHandshakeRouteHandler : IMqttRouteHandler
    {
        public string Route => RouteKeys.DeviceApplicationHandshakeResultRoute;

        public Task Handle(string payload, string contentType, IDictionary<string, string> brewerProperties)
        {
            Log.Information(@"Message arrived: Route => {Route}, 
                                Payload => {Payload}, 
                                ContentType => {ContentType}
                                brewerProperties => {brewerProperties}",
                                Route,
                                payload,
                                contentType,
                                brewerProperties);

            return Task.CompletedTask;
        }
    }
}
