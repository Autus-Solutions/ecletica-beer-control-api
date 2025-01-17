using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Persistence.EF.Mapping.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcleticaBeerControl.Persistence.EF.Mapping
{
    internal sealed class DeviceMapping : BreweryEntityMapping<Device>
    {
        public override void Configure(EntityTypeBuilder<Device> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Identifier);

            builder.Property(x => x.Identifier)
                    .IsRequired();
            builder.Property(x => x.Name).
                    HasMaxLength(100)
                    .IsRequired();
            builder.Property(x => x.Description)
                    .HasMaxLength(200);
            builder.Property(x => x.Status)
                    .IsRequired();

            builder.HasIndex(x => x.Identifier)
                    .IsUnique();

        }
    }
}
