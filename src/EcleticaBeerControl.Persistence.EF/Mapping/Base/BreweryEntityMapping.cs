using EcleticaBeerControl.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcleticaBeerControl.Persistence.EF.Mapping.Base
{
    internal abstract class BreweryEntityMapping<TBreweryEntity> : EntityMapping<TBreweryEntity>, IEntityTypeConfiguration<TBreweryEntity>
        where TBreweryEntity : BreweryEntity
    {
        public override void Configure(EntityTypeBuilder<TBreweryEntity> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Brewery)
                .WithMany()
                .HasForeignKey(x => x.BreweryId);
        }
    }
}
