using EcleticaBeerControl.Api.Controllers;
using EcleticaBeerControl.Persistence.EF.Database;
using Microsoft.EntityFrameworkCore;

namespace EcleticaBeerControl.Api.Extensions
{
    public static class AppExtensions
    {
        public static void MapControllersEndpoints(this WebApplication app)
        {
            BreweryEndpoints.AddRoutes(app);
            DeviceEndpoints.AddRoutes(app);
        }

        public static async Task MigrateDatabaseIfNeededAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

            if (db is not null)
                await db.Database.MigrateAsync();

        }
    }
}
