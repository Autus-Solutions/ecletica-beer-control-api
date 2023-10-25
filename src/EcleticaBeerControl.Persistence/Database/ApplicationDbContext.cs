using EcleticaBeerControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcleticaBeerControl.Persistence.Database
{
    public sealed class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Device> Devices { get; set; }
    }
}
