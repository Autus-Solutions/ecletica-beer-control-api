using EcleticaBeerMonitor.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton(NLog.LogManager.GetCurrentClassLogger());
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();