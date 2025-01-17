using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcleticaBeerControl.Persistence.EF.Database
{
    public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<FermentationProfile> FermentationProfiles { get; set; }
        public DbSet<FermentationDefinition> FermentationDefinitions { get; set; }
        public DbSet<FermentationSession> FermentationSessions { get; set; }
    }
}
