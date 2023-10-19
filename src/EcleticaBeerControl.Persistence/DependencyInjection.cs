using EcleticaBeerControl.Domain.Repositories;
using EcleticaBeerControl.Persistence.Database.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcleticaBeerControl.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton((provider) => new Supabase.Client(
                    configuration["SupabaseProjectUrl"] ?? string.Empty,
                    configuration["SupabaseProjectKey"],
                    new Supabase.SupabaseOptions
                    {
                        AutoRefreshToken = bool.TryParse(configuration["SupabaseClientAutoRefreshToken"], out var autoRefresh),
                        AutoConnectRealtime = true,
                    })
                );

            services.AddScoped<IDeviceRepository, DeviceRepository>();

            return services;
        }
    }
}
