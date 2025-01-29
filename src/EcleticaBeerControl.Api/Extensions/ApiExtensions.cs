using Asp.Versioning.Builder;
using EcleticaBeerControl.Api.Endpoints;
using EcleticaBeerControl.Persistence.EF.Database;
using Microsoft.EntityFrameworkCore;

namespace EcleticaBeerControl.Api.Extensions
{
    public static class ApiExtensions
    {
        public static void MapApplicationEndpoints(this IEndpointRouteBuilder app)
        {
            ApiVersionSet apiVersionSet = app.NewApiVersionSet()
                        .HasApiVersion(new Asp.Versioning.ApiVersion(1))
                        .ReportApiVersions()
                        .Build();

            var versionedGroup = app.MapGroup("api/v{apiVersion:apiVersion}")
                            .WithApiVersionSet(apiVersionSet);

            versionedGroup.MapDeviceEndpoints();
            versionedGroup.MapBreweryEndpoints();
        }

        public static async Task ApplyMigrationsIfNeededAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

            if (db is not null)
                await db.Database.MigrateAsync();
        }
    }
}
