using EcleticaBeerControl.Api;
using EcleticaBeerControl.Api.Extensions;
using EcleticaBeerControl.Api.Middlewares;
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
                    .AddEFPersistence()
                    .AddApplication();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    if (app.Environment.IsProduction())
    {
        app.UseHttpsRedirection();
    }

    app.UseGlobalErrorHandling();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.MapControllersEndpoints();
    app.MapIdentityApi<User>();
    app.UseResponseCompression();

    await app.MigrateDatabaseIfNeededAsync();
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

