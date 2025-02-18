using EcleticaBeerControl.Domain.DomainEvents;
using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Persistence.EF.Mapping.Timeseries;
using Microsoft.EntityFrameworkCore;

namespace EcleticaBeerControl.Persistence.EF.Database
{
    public sealed class TimeseriesDbContext(DbContextOptions<TimeseriesDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TemperatureMapping());
            modelBuilder.Ignore<DomainEvent>();
        }

        public DbSet<Temperature> Temperatures { get; set; }
    }
}
