using EcleticaBeerControl.Domain.Repositories;
using EcleticaBeerControl.Infrastructure.Database.Repositories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcleticaBeerControl.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped((provider) => new Supabase.Client(
                configuration["SupabaseProjectUrl"] ?? string.Empty,
                configuration["SupabaseProjectKey"] ?? configuration["SupabaseProjectServiceRoleSecretKey"] ?? string.Empty,
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
