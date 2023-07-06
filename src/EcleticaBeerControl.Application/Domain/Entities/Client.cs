using EcleticaBeerControl.Application.Domain.Entities.Base;
using Postgrest.Attributes;

namespace EcleticaBeerControl.Application.Domain.Entities
{
    [Table("clients")]
    public class Client : BaseEntity
    {
        [Column("logo")]
        public string Logo { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
}
