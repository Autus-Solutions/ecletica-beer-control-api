using EcleticaBeerControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcleticaBeerControl.Persistence.EF.Mapping.Timeseries
{
    internal sealed class TemperatureMapping : IEntityTypeConfiguration<Temperature>
    {
        public void Configure(EntityTypeBuilder<Temperature> builder)
        {
            builder.ToTable("temperatures");

            builder.HasNoKey();

            builder.Property(x => x.Time).HasColumnName("time");
            builder.Property(x => x.DeviceId).HasColumnName("device_id");
            builder.Property(x => x.Value).HasColumnName("value");
        }
    }
}
