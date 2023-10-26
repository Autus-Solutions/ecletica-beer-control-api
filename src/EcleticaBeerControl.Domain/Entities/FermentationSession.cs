using EcleticaBeerControl.Domain.Entities.Base;
using EcleticaBeerControl.Domain.Enums;
using Postgrest.Attributes;

namespace EcleticaBeerControl.Domain.Entities
{
    [Table("fermentation_sessions")]
    public class FermentationSession : BreweryEntity
    {
        [Column("device_identifier")]
        public string DeviceIdentifier { get; set; } = string.Empty;

        [Column("current_temperature")]
        public int? CurrentTemperature { get; set; }

        [Column("target_temperature")]
        public int? TargetTemperature { get; set; }

        [Column("current_step_title")]
        public string? CurrentStepTitle { get; set; }

        [Column("current_step_start")]
        public DateTime? CurrentStepStart { get; set; }

        [Column("current_step_end")]
        public DateTime? CurrentStepEnd { get; set; }

        [Column("current_step_total_days")]
        public int? CurrentStepTotalDays { get; set; }

        [Column("started_at")]
        public DateTime? StartedAt { get; set; }

        [Column("finished_at")]
        public DateTime? FinishedAt { get; set; }

        [Column("status")]
        public FementationSessionStatus Status { get; set; } = FementationSessionStatus.Draft;
        public virtual Device Device { get; set; }

        public static FermentationSession Create(Guid breweryId,
                string deviceIdentifier,
                int currentTemperature,
                int targetTemperature,
                string currentStepTitle,
                DateTime currentStepStart,
                DateTime currentStepEnd,
                int currentStepTotalDays,
                Guid createdBy)
        {
            var entity = new FermentationSession
            {
                BreweryId = breweryId,
                DeviceIdentifier = deviceIdentifier,
                CurrentTemperature = currentTemperature,
                TargetTemperature = targetTemperature,
                CurrentStepTitle = currentStepTitle,
                CurrentStepStart = currentStepStart,
                CurrentStepEnd = currentStepEnd,
                CurrentStepTotalDays = currentStepTotalDays,
                CreateBy = createdBy
            };

            return entity;
        }
    }
}
