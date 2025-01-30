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

        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var identitySchemaDb = scope.ServiceProvider.GetService<IdentityDbContext>();
            using var applicationSchemaDb = scope.ServiceProvider.GetService<ApplicationDbContext>();

            if (identitySchemaDb is not null)
                identitySchemaDb.Database.Migrate();

            if (applicationSchemaDb is not null)
                applicationSchemaDb.Database.Migrate();
        }
    }
}
