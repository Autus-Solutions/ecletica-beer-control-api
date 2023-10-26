using EcleticaBeerControl.Domain.Enums;

namespace EcleticaBeerControl.Domain.ValueObjects.Fermentation
{
    public class FermentationProfileStep
    {
        public required int Order { get; init; }
        public required string Title { get; init; }
        public required int TargetTemperature { get; init; }
        public required int TotalDays { get; init; }
        public required int TotalDaysToReachTargetTemperature { get; init; }
        public required FermentationProfileStepType Type { get; init; } = FermentationProfileStepType.Primary;
    }
}
