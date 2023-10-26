using EcleticaBeerControl.Domain.Entities.Base;
using Postgrest.Attributes;
using System.Text.Json;

namespace EcleticaBeerControl.Domain.Entities
{
    [Table("fermentation_definitions")]
    public class FermentationDefinition : BreweryEntity
    {
        [Column("device_identifier")]
        public string DeviceIdentifier { get; set; } = string.Empty;

        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("fermentation_profile")]
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
