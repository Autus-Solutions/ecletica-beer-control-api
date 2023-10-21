namespace EcleticaBeerControl.Infrastructure.Mqtt
{
    public interface IMqttMessageRouter
    {
        Task Initialize(CancellationToken cancellationToken = default);
    }
}
