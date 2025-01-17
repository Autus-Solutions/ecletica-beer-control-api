using EcleticaBeerControl.Domain.Contexts;
using EcleticaBeerControl.Domain.ValueObjects;

namespace EcleticaBeerControl.Application.Members.Services.Fermentation
{
    public sealed class FermentationSessionExecutionContextGenerator
    {
        public FermentationSessionExecutionContext Generate(Guid breweryId, string deviceIdentifier, IEnumerable<FermentationProfileStep> steps)
        {
            var context = new FermentationSessionExecutionContext();
            var orderedStepsEnumerator = steps.OrderBy(s => s.Order).ToList().GetEnumerator();

            context.Step.NextStep = RegisterNewStep(breweryId, deviceIdentifier, orderedStepsEnumerator);

            return context;
        }

        private FermentationSessionExecutionStep? RegisterNewStep(Guid breweryId, string deviceIdentifier, IEnumerator<FermentationProfileStep> steps)
        {
            if (steps.MoveNext())
            {
                var newStep = new FermentationSessionExecutionStep
                {
                    BreweryId = breweryId,
                    DeviceIdentifier = deviceIdentifier,
                    Title = steps.Current.Type.ToString(),
                    TargetTemperature = steps.Current.TargetTemperature,
                    WaitForUntilStartNextStepInDays = TimeSpan.FromDays(steps.Current.TotalDaysToReachTargetTemperature),
                    WaitForUntilCallNextStepInDays = TimeSpan.FromDays(steps.Current.TotalDays),
                    NextStep = RegisterNewStep(breweryId, deviceIdentifier, steps)
                };

                return newStep;
            }

            return null;
        }
    }
}
