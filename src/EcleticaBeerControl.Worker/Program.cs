using EcleticaBeerControl.Infrastructure;
using EcleticaBeerControl.Persistence;
using EcleticaBeerControl.Worker;
using Serilog;
using System.Globalization;

var defaultCultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = defaultCultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = defaultCultureInfo;

IHost host = Host.CreateDefaultBuilder(args)
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
                .AddPersistence(builder.Configuration)
                .AddWorker(builder.Configuration)
                .AddHostedService<Worker>();
    })
    .UseSerilog((context, lc) => lc.ReadFrom.Configuration(context.Configuration))
    .Build();

host.Run();
