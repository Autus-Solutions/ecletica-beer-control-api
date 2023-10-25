using EcleticaBeerControl.Persistence.Supabase.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Supabase;

namespace EcleticaBeerControl.Persistence.Supabase
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSupabasePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped((provider) => new Client(
                configuration["SupabaseProjectUrl"] ?? string.Empty,
                configuration["SupabaseProjectSecretKey"] ?? string.Empty,
                new SupabaseOptions
                {
                    AutoRefreshToken = bool.TryParse(configuration["SupabaseClientAutoRefreshToken"], out var autoRefresh),
                    AutoConnectRealtime = true,
                })
            );

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
