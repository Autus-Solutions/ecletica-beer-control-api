using EcleticaBeerControl.Domain.Entities;

namespace EcleticaBeerControl.Worker.Adapters
{
    internal sealed class TemperatureAdapter
    {
        public static Temperature Adapt(string deviceIdentifier, Payloads.Temperature temperature)
        {
            return new Temperature
            {
                Time = DateTime.UtcNow,
                DeviceId = deviceIdentifier,
                Value = temperature.Degrees,
                CreateBy = "ebc-worker"
            };
        }
    }
}
