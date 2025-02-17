using EcleticaBeerControl.Infrastructure;
using EcleticaBeerControl.Persistence.EF;
using EcleticaBeerControl.Worker;
using Serilog;
using SerilogTracing;
using System.Globalization;

var defaultCultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = defaultCultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = defaultCultureInfo;

try
{
    using var listener = new ActivityListenerConfiguration()
            .Instrument.WithDefaultInstrumentation(true)
            .Instrument.HttpClientRequests()
            .TraceToSharedLogger();

    var host = Host.CreateDefaultBuilder(args)
        .UseEnvironment(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development")
        .ConfigureHostConfiguration(builder =>
        {
            builder.AddUserSecrets<Program>();
        })
        .ConfigureServices((builder, services) =>
        {
            services.Configure<HostOptions>(hostOptions =>
            {
                hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
            });

            services.AddInfrastructure(builder.Configuration)
                    .AddEFPersistence(builder.HostingEnvironment)
                    .AddTimeseriesEFPersistence(builder.HostingEnvironment)
                    .AddWorker(builder.Configuration);

            services.AddHostedService<Worker>();
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
