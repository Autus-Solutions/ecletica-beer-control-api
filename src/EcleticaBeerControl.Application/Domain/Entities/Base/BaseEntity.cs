using Postgrest.Attributes;
using Postgrest.Models;

namespace EcleticaBeerControl.Application.Domain.Entities.Base
{
    public abstract class BaseEntity : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}