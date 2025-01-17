using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Persistence.EF.Mapping.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcleticaBeerControl.Persistence.EF.Mapping
{
    internal sealed class FermentationDefinitionMapping : BreweryEntityMapping<FermentationDefinition>
    {
        public override void Configure(EntityTypeBuilder<FermentationDefinition> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Device)
                .WithMany()
                .HasForeignKey(x => x.DeviceIdentifier);

            builder.Property(x => x.Title).
                    HasMaxLength(100)
                    .IsRequired();

            builder.Property(x => x.Description)
                    .HasMaxLength(200);

            builder.Property(x => x.FermentationProfile)
                    .HasColumnType("jsonb")
                    .IsRequired();
        }
    }
}
