using EcleticaBeerControl.Domain.Entities.Base;

namespace EcleticaBeerControl.Domain.Entities
{
    public class Brewery : Entity
    {
        public string? Logo { get; init; }
        public string Name { get; init; } = string.Empty;

        public static Brewery Register(Guid id, string name, Guid createdBy)
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
