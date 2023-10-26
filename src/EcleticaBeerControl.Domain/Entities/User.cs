using EcleticaBeerControl.Domain.Entities.Base;
using Postgrest.Attributes;

namespace EcleticaBeerControl.Domain.Entities
{
    [Table("users")]
    public class User : Entity
    {
        [Column("name")]
        public string Name { get; init; } = string.Empty;

        public static User Register(Guid id, string name, Guid createdBy)
        {
            var entity = new User
            {
                Id = id,
                Name = name,
                CreateBy = createdBy,
            };

            return entity;
        }
    }
}
