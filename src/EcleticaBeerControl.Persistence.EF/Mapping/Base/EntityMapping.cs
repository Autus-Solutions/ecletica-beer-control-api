using EcleticaBeerControl.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcleticaBeerControl.Persistence.EF.Mapping.Base
{
    internal abstract class EntityMapping<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasQueryFilter(x => x.DeletedBy == null);

            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.BaseUrl);
            builder.Ignore(x => x.RequestClientOptions);
            builder.Ignore(x => x.TableName);
            builder.Ignore(x => x.PrimaryKey);
        }
    }
}
