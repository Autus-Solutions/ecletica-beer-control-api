using EcleticaBeerControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcleticaBeerControl.Persistence.EF.Database
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Device> Devices { get; set; }
    }
}
