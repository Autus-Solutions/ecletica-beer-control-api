using EcleticaBeerControl.Persistence.Database;
using EcleticaBeerControl.Persistence.Database.Options;
using EcleticaBeerControl.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EcleticaBeerControl.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped((provider) => new Supabase.Client(
                configuration["SupabaseProjectUrl"] ?? string.Empty,
                configuration["SupabaseProjectSecretKey"] ?? string.Empty,
                new Supabase.SupabaseOptions
                {
                    AutoRefreshToken = bool.TryParse(configuration["SupabaseClientAutoRefreshToken"], out var autoRefresh),
                    AutoConnectRealtime = true,
                })
            );

            services.ConfigureOptions<DatabaseOptionsSetup>();

            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var databaseOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>()!.Value;

                options.UseNpgsql(databaseOptions.ConnectionString, options =>
                {
                    options.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    options.CommandTimeout(databaseOptions.CommandTimeout);
                });

                options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            });

            services.Scan(scan => scan
                  .FromAssemblyOf<DeviceRepository>()
                      .AddClasses(classes => classes.Where(type => type.FullName!.EndsWith("Repository")))
                      .AsImplementedInterfaces()
                      .AsSelf()
                      .WithScopedLifetime());

            return services;
        }
    }
}
