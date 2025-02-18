using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Persistence.EF.Mapping.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcleticaBeerControl.Persistence.EF.Mapping.Timeseries
{
    internal sealed class TemperatureMapping : EntityMapping<Temperature>
    {
        public override void Configure(EntityTypeBuilder<Temperature> builder)
        {
            base.Configure(builder);

            builder.ToTable("temperatures");
            builder.HasKey(x => new { x.DeviceId, x.Time });

            builder.Ignore(x => x.Id);

            builder.Property(x => x.Time).HasColumnName("time");
            builder.Property(x => x.DeviceId).HasColumnName("device_id");
            builder.Property(x => x.Value).HasColumnName("value");

            builder.Ignore(x => x.CreatedAt);
            builder.Ignore(x => x.CreateBy);
            builder.Ignore(x => x.UpdatedAt);
            builder.Ignore(x => x.UpdatedBy);
            builder.Ignore(x => x.DeletedAt);
            builder.Ignore(x => x.DeletedBy);
        }
    }
}
