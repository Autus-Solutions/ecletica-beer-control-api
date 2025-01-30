using EcleticaBeerControl.Domain.DomainEvents;
using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Interfaces;
using EcleticaBeerControl.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace EcleticaBeerControl.Persistence.EF.Database
{
    public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IBreweryResolver breweryService) : DbContext(options)
    {
        private readonly IBreweryResolver breweryService = breweryService;
        private string CurrentBreweryId => breweryService.BreweryId!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("ebc");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<Device>().HasQueryFilter(x => x.BreweryId == CurrentBreweryId);
            modelBuilder.Entity<FermentationDefinition>().HasQueryFilter(x => x.BreweryId == CurrentBreweryId);
            modelBuilder.Entity<FermentationProfile>().HasQueryFilter(x => x.BreweryId == CurrentBreweryId);
            modelBuilder.Entity<FermentationSession>().HasQueryFilter(x => x.BreweryId == CurrentBreweryId);

            modelBuilder.Ignore<DomainEvent>();
        }

        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<FermentationProfile> FermentationProfiles { get; set; }
        public DbSet<FermentationDefinition> FermentationDefinitions { get; set; }
        public DbSet<FermentationSession> FermentationSessions { get; set; }

        public override int SaveChanges()
        {
            InterceptSaveChangesForBreweriesBasedEntities();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            InterceptSaveChangesForBreweriesBasedEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            InterceptSaveChangesForBreweriesBasedEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            InterceptSaveChangesForBreweriesBasedEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void InterceptSaveChangesForBreweriesBasedEntities()
        {
            foreach (var entry in ChangeTracker.Entries<IBreweryEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.BreweryId = CurrentBreweryId;
                        break;
                }
            }
        }
    }
}
