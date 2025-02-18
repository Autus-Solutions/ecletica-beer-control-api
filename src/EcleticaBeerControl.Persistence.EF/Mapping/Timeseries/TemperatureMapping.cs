using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Persistence.EF.Mapping.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcleticaBeerControl.Persistence.EF.Mapping.Timeseries
{
    internal sealed class TemperatureMapping : EntityMapping<Temperature>
    {
        public override void Configure(EntityTypeBuilder<Temperature> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
        }
    }
}
