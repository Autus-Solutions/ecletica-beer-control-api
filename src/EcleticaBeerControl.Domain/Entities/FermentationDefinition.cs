using EcleticaBeerControl.Domain.Entities.Base;
using System.Text.Json;

namespace EcleticaBeerControl.Domain.Entities
{
    public class FermentationDefinition : BreweryEntity
    {
        public required string DeviceIdentifier { get; set; } = string.Empty;
        public required string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public required JsonDocument FermentationProfile { get; init; } = JsonDocument.Parse("{}");
        public virtual Device? Device { get; set; }

        public static FermentationDefinition Create(string breweryId,
                string deviceIdentifier,
                string title,
                string? description,
                JsonDocument profile,
                string createdBy)
        {
            var entity = new FermentationDefinition
            {
                BreweryId = breweryId,
                DeviceIdentifier = deviceIdentifier,
                Title = title,
                Description = description,
                FermentationProfile = profile,
                CreateBy = createdBy
            };

            return entity;
        }
    }
}
