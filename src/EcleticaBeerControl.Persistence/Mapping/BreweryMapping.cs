using EcleticaBeerControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcleticaBeerControl.Persistence.Mapping
{
    internal sealed class BreweryMapping : IEntityTypeConfiguration<Brewery>
    {
        public void Configure(EntityTypeBuilder<Brewery> builder)
        {
            builder.ToTable("breweries");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Logo);
            builder.Property(x => x.Name)
                    .HasMaxLength(200);

            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.BaseUrl);
            builder.Ignore(x => x.RequestClientOptions);
            builder.Ignore(x => x.TableName);
            builder.Ignore(x => x.PrimaryKey);

        }
    }
}
