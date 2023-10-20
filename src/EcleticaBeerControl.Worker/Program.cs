using EcleticaBeerControl.Application;
using EcleticaBeerControl.Infrastructure;
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
        services.AddInfrastructure(builder.Configuration)
                .AddApplication()
                .AddWorker(builder.Configuration)
                .AddHostedService<Worker>();
    })
    .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
    .Build();

host.Run();
