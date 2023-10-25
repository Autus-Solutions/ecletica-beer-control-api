using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Persistence.EF.Mapping.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcleticaBeerControl.Persistence.EF.Mapping
{
    internal sealed class BreweryMapping : EntityMapping<Brewery>
    {
        public override void Configure(EntityTypeBuilder<Brewery> builder)
        {
            base.Configure(builder);

            builder.ToTable("breweries");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Logo);
            builder.Property(x => x.Name)
                    .HasMaxLength(200);
        }
    }
}
