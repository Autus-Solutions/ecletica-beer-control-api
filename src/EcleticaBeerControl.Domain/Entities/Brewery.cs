using EcleticaBeerControl.Domain.Entities.Base;

namespace EcleticaBeerControl.Domain.Entities
{
    public class Brewery : Entity
    {
        public string? Logo { get; init; }
        public required string Name { get; init; } = string.Empty;

        public static Brewery Register(string id, string name, string createdBy)
        {
            var entity = new Brewery
            {
                Id = id,
                Name = name,
                CreateBy = createdBy
            };

            return entity;
        }
    }
}
