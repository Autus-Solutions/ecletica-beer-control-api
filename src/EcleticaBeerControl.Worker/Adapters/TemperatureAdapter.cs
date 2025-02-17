using EcleticaBeerControl.Domain.Entities;

namespace EcleticaBeerControl.Worker.Adapters
{
    internal sealed class TemperatureAdapter
    {
        public static Temperature Adapt(string deviceIdentifier, Payloads.Temperature temperature)
        {
            return new Temperature
            {
                Time = DateTime.Now,
                DeviceId = deviceIdentifier,
                Value = temperature.Degrees
            };
        }
    }
}
