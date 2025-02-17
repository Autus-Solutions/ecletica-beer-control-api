namespace EcleticaBeerControl.Domain.Entities
{
    public class Temperature
    {
        public required DateTime Time { get; init; }
        public required string DeviceId { get; init; } = string.Empty;
        public double? Value { get; init; }
    }
}
