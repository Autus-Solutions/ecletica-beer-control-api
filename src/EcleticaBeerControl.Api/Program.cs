using Asp.Versioning.ApiExplorer;
using EcleticaBeerControl.Api;
using EcleticaBeerControl.Api.Extensions;
using EcleticaBeerControl.Api.Middlewares;
using EcleticaBeerControl.Domain.Models;
using EcleticaBeerControl.Infrastructure;
using EcleticaBeerControl.Persistence.EF;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using SerilogTracing;
using System.Globalization;

var defaultCultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = defaultCultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = defaultCultureInfo;

try
{
    using var listener = new ActivityListenerConfiguration()
            .Instrument.AspNetCoreRequests()
            .Instrument.HttpClientRequests()
            .TraceToSharedLogger();

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddInfrastructure(builder.Configuration)
                    .AddEFPersistence()
                    .AddTimeseriesEFPersistence()
                    .AddApplication();

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

        foreach (ApiVersionDescription description in descriptions)
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();

            options.SwaggerEndpoint(url, name);
        }
    });

    if (app.Environment.IsProduction())
        app.UseHttpsRedirection();

    app.UseBreweryResolver();
    app.UseGlobalErrorHandling();

    app.MapControllers();
    app.MapApplicationEndpoints();
    app.MapIdentityApi<User>();

    app.MapHealthChecks("health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.UseResponseCompression();
    app.ApplyMigrations();

    await app.RunAsync();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled Exception");
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}

