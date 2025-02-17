using EcleticaBeerControl.Worker.Payloads.Enums;

namespace EcleticaBeerControl.Worker.Payloads
{
    public sealed record DeviceTemperatureMetric
    {
        public double Degrees { get; set; }
        public DegreeFormat DegreeFormat { get; set; }
    }
}
