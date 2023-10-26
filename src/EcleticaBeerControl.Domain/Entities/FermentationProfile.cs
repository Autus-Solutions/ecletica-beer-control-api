using EcleticaBeerControl.Domain.Entities.Base;
using Postgrest.Attributes;
using System.Text.Json;

namespace EcleticaBeerControl.Domain.Entities
{
    [Table("fermentation_profiles")]
    public class FermentationProfile : BreweryEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("steps")]
        public JsonDocument Steps { get; set; } = JsonDocument.Parse("[]");

        public static FermentationProfile Create(Guid breweryId, string name, string? description, JsonDocument profileSteps, Guid createdBy)
        {
            var entity = new FermentationProfile
            {
                BreweryId = breweryId,
                Name = name,
                Description = description,
                Steps = profileSteps,
                CreateBy = createdBy
            };

            return entity;
        }
    }
}
