using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Persistence.EF.Mapping.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcleticaBeerControl.Persistence.EF.Mapping
{
    internal sealed class FermentationSessionMapping : BreweryEntityMapping<FermentationSession>
    {
        public override void Configure(EntityTypeBuilder<FermentationSession> builder)
        {
            base.Configure(builder);

            builder.ToTable("fermentation_sessions");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Device)
                .WithMany()
                .HasForeignKey(x => x.DeviceIdentifier);
        }
    }
}
