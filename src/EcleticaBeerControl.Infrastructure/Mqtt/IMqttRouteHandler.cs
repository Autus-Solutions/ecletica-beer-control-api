namespace EcleticaBeerControl.Infrastructure.Mqtt
{
    public interface IMqttRouteHandler
    {
        string Route { get; }
        Task Handle(string payload, string contentType, IDictionary<string, string> userProperties);
    }
}