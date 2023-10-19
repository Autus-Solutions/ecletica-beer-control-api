using EcleticaBeerControl.Infrastructure;
using EcleticaBeerControl.Persistence;
using EcleticaBeerControl.Worker;
using Serilog;
using System.Globalization;

var defaultCultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = defaultCultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = defaultCultureInfo;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostBuilderContext, services) =>
    {
        services.AddInfrastructure(hostBuilderContext.Configuration)
                .AddPersistence(hostBuilderContext.Configuration)
                .AddWorker(hostBuilderContext.Configuration)
                .AddHostedService<Worker>();
    })
    .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
    .Build();

host.Run();
