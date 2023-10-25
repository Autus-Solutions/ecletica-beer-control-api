using EcleticaBeerControl.Infrastructure;
using EcleticaBeerControl.Persistence.EF;
using EcleticaBeerControl.Worker;
using Serilog;
using System.Globalization;

var defaultCultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = defaultCultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = defaultCultureInfo;

IHost host = Host.CreateDefaultBuilder(args)
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
                .AddEFPersistence(builder.Configuration)
                .AddWorker(builder.Configuration);

        services.AddHostedService<Worker>();
    })
    .UseDefaultServiceProvider(options => options.ValidateScopes = false)
    .UseSerilog((context, lc) => lc.ReadFrom.Configuration(context.Configuration))
    .Build();

host.Run();
