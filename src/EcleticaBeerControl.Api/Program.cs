using EcleticaBeerControl.Api;
using EcleticaBeerControl.Api.Middlewares;
using EcleticaBeerControl.Application;
using EcleticaBeerControl.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration)
                .AddApplication()
                .AddPresentation(builder.Configuration);

builder.Host.UseSerilog((context, configuration)
    => configuration.ReadFrom.Configuration(context.Configuration));

builder.Host.ConfigureServices((hostBuilderContext, services) =>
{
    services.AddHostedService<BackgroundWorker>();
});

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseSupabaseAuth();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseResponseCompression();

app.Run();
