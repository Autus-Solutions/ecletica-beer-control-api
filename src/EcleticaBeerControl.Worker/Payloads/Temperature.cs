using EcleticaBeerControl.Worker.Payloads.Enums;

namespace EcleticaBeerControl.Worker.Payloads
{
    public sealed record Temperature
    {
        public double Degrees { get; set; }
        public DegreeFormat DegreeFormat { get; set; }
    }
}
