using Carter;
using EcleticaBeerControl.Api;
using EcleticaBeerControl.Api.Middlewares;
using EcleticaBeerControl.Application;
using EcleticaBeerControl.Infrastructure;
using EcleticaBeerControl.Persistence;
using Serilog;
using System.Globalization;

var defaultCultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = defaultCultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = defaultCultureInfo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();

builder.Services.AddInfrastructure(builder.Configuration)
                .AddPersistence(builder.Configuration)
                .AddApplication()
                .AddPresentation(builder.Configuration);

builder.Host.UseSerilog((context, lc) => lc.ReadFrom.Configuration(context.Configuration));

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

app.UseGlobalErrorHandling();
app.UseSupabaseAuth();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapCarter();
app.UseResponseCompression();

app.Run();
