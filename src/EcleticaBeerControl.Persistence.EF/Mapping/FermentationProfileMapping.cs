using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Persistence.EF.Mapping.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcleticaBeerControl.Persistence.EF.Mapping
{
    internal sealed class FermentationProfileMapping : BreweryEntityMapping<FermentationProfile>
    {
        public override void Configure(EntityTypeBuilder<FermentationProfile> builder)
        {
            base.Configure(builder);

            builder.ToTable("fermentation_profiles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).
                    HasMaxLength(100)
                    .IsRequired();
            builder.Property(x => x.Description)
                    .HasMaxLength(200);
            builder.Property(x => x.Steps)
                    .HasColumnType("jsonb")
                    .IsRequired();
        }
    }
}
