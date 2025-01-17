using EcleticaBeerControl.Domain.Entities.Base;
using System.Text.Json;

namespace EcleticaBeerControl.Domain.Entities
{
    public class FermentationDefinition : BreweryEntity
    {
        public string DeviceIdentifier { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public JsonDocument FermentationProfile { get; init; } = JsonDocument.Parse("{}");
        public virtual Device Device { get; set; }

        public static FermentationDefinition Create(Guid breweryId,
                string deviceIdentifier,
                string title,
                string? description,
                JsonDocument profile,
                Guid createdBy)
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
