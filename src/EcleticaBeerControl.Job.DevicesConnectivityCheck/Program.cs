using EcleticaBeerControl.DevicesConnectivityCheck;
using Serilog;
using Serilog.Events;
using Serilog.Templates.Themes;
using SerilogTracing.Expressions;
using SerilogTracing;
using System.Globalization;

var defaultCultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = defaultCultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = defaultCultureInfo;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .Enrich.WithProperty("Application", "Ecletica Beer Control")
    .WriteTo.Console(Formatters.CreateConsoleTextFormatter(theme: TemplateTheme.Code))
    .CreateLogger();

using var listener = new ActivityListenerConfiguration()
    .Instrument.AspNetCoreRequests()
    .TraceToSharedLogger();

Log.Information("Starting UP");

try
{
    IHostBuilder host = Host.CreateDefaultBuilder(args)
    .UseEnvironment(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development")
    .ConfigureHostConfiguration(builder =>
    {
        builder.AddUserSecrets<Program>();
    })
    .ConfigureServices((builder, services) =>
    {
        services.AddSerilog();

        services//.AddInfrastructure(builder.Configuration)
                //.AddPersistence(builder.Configuration)
                //.AddWorker(builder.Configuration)
                .AddHostedService<Job>();
    })
    .UseDefaultServiceProvider(options => options.ValidateScopes = false);

    var app = host.Build();
    await app.RunAsync();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}


