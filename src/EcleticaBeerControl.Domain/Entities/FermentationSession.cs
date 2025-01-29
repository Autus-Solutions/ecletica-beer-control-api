using EcleticaBeerControl.Domain.Entities.Base;
using EcleticaBeerControl.Domain.Enums;

namespace EcleticaBeerControl.Domain.Entities
{
    public class FermentationSession : BreweryEntity
    {
        public string DeviceIdentifier { get; set; } = string.Empty;
        public int? CurrentTemperature { get; set; }
        public int? TargetTemperature { get; set; }
        public string? CurrentStepTitle { get; set; }
        public DateTime? CurrentStepStart { get; set; }
        public DateTime? CurrentStepEnd { get; set; }
        public int? CurrentStepTotalDays { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public FementationSessionStatus Status { get; set; } = FementationSessionStatus.Draft;
        public virtual Device Device { get; set; }

        public static FermentationSession Create(string breweryId,
                string deviceIdentifier,
                string createdBy)
        {
            var entity = new FermentationSession
            {
                BreweryId = breweryId,
                DeviceIdentifier = deviceIdentifier,
                CreateBy = createdBy
            };

            return entity;
        }
    }
}
