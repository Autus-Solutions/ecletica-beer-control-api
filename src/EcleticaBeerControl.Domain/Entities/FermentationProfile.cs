using EcleticaBeerControl.Domain.Entities.Base;
using System.Text.Json;

namespace EcleticaBeerControl.Domain.Entities
{
    public class FermentationProfile : BreweryEntity
    {
        public required string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public required JsonDocument Steps { get; set; } = JsonDocument.Parse("[]");

        public static FermentationProfile Create(string breweryId, string name, string? description, JsonDocument profileSteps, string createdBy)
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
