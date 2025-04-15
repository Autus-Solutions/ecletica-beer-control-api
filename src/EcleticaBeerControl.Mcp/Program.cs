using System.Globalization;
using SerilogTracing;
using Serilog;

var defaultCultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = defaultCultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = defaultCultureInfo;

try
{
    using var listener = new ActivityListenerConfiguration()
            .Instrument.WithDefaultInstrumentation(true)
            .Instrument.HttpClientRequests()
            .TraceToSharedLogger();

    var builder = WebApplication.CreateBuilder(args);

    builder.Services
            .AddMcpServer()
            .WithToolsFromAssembly();

    var app = builder.Build();
    app.MapMcp();

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
