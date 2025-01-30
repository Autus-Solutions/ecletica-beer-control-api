using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using EcleticaBeerControl.Api;
using EcleticaBeerControl.Api.Extensions;
using EcleticaBeerControl.Api.Middlewares;
using EcleticaBeerControl.Api.OpenApi;
using EcleticaBeerControl.Domain.Models;
using EcleticaBeerControl.Infrastructure;
using EcleticaBeerControl.Persistence.EF;
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
    builder.Services.AddSerilog();

    builder.Services.AddInfrastructure(builder.Configuration)
                    .AddEFPersistence(builder.Environment)
                    .AddApplication();

    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1);
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });

    builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();

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

    await app.ApplyMigrationsIfNeededAsync();

    app.UseHttpsRedirection();
    app.UseBreweryResolver();
    app.UseGlobalErrorHandling();
    app.MapControllers();
    app.MapApplicationEndpoints();
    app.MapIdentityApi<User>();
    app.UseResponseCompression();

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

