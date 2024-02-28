namespace EcleticaBeerControl.Domain.Contexts
{
    public class FermentationSessionExecutionContext
    {
        public FermentationSessionExecutionContext()
        {
            Step = new FermentationSessionExecutionStep
            {
                Title = "Iniciando processo de fermentação"
            };
        }
        public FermentationSessionExecutionStep Step;
    }

    public class FermentationSessionExecutionStep
    {
        public Guid BreweryId { get; init; }
        public string DeviceIdentifier { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public int TargetTemperature { get; init; }
        public TimeSpan WaitForUntilStartNextStepInDays { get; init; } = TimeSpan.Zero;
        public TimeSpan WaitForUntilCallNextStepInDays { get; init; } = TimeSpan.Zero;
        public FermentationSessionExecutionStep? NextStep { get; set; }
    }
}
