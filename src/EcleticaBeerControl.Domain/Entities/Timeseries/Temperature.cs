using EcleticaBeerControl.Domain.Entities.Base;

namespace EcleticaBeerControl.Domain.Entities
{
    public class Temperature : Entity
    {
        public required DateTime Time { get; init; } = DateTime.UtcNow;
        public required string DeviceId { get; init; } = string.Empty;
        public double? Value { get; init; }
    }
}
