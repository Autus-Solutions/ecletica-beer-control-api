using EcleticaBeerControl.Domain.Repositories;
using EcleticaBeerControl.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddScoped<IDeviceRepository, DeviceRepository>();

            return services;
        }
    }
}
