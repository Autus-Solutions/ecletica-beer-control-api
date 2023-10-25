using EcleticaBeerControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcleticaBeerControl.Persistence.Mapping
{
    internal sealed class DeviceMapping : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("devices");
            builder.HasKey(x => x.Id);
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
            builder.HasQueryFilter(x => x.DeletedBy == null);

            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.BaseUrl);
            builder.Ignore(x => x.RequestClientOptions);
            builder.Ignore(x => x.TableName);
            builder.Ignore(x => x.PrimaryKey);
        }
    }
}
