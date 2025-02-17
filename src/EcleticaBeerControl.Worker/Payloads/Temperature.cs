using EcleticaBeerControl.Worker.Payloads.Enums;

namespace EcleticaBeerControl.Worker.Payloads
{
    public sealed record Temperature
    {
        public long Degrees { get; set; }
        public DegreeFormat DegreeFormat { get; set; }
    }
}
