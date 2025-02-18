using EcleticaBeerControl.Persistence.EF.Database;
using EcleticaBeerControl.Persistence.EF.Database.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EcleticaBeerControl.Persistence.EF
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEFPersistence(this IServiceCollection services)
        {
            services.ConfigureOptions<DatabaseOptionsSetup>();

            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var databaseOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseNpgsql(databaseOptions.ConnectionString, options =>
                {
                    options.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    options.CommandTimeout(databaseOptions.CommandTimeout);
                });

#if DEBUG
                options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
#endif

            });

            services.AddDbContext<IdentityDbContext>((provider, options) =>
            {
                var databaseOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                options.UseNpgsql(databaseOptions.ConnectionString, options =>
                {
                    options.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    options.CommandTimeout(databaseOptions.CommandTimeout);
                });

            });

            return services;
        }

        public static IServiceCollection AddTimeseriesEFPersistence(this IServiceCollection services)
        {
            services.ConfigureOptions<TimeseriesDatabaseOptionsSetup>();

            services.AddDbContext<TimeseriesDbContext>((provider, options) =>
            {
                var databaseOptions = provider.GetRequiredService<IOptions<TimeseriesDatabaseOptions>>().Value;
                options.UseNpgsql(databaseOptions.ConnectionString, options =>
                {
                    options.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    options.CommandTimeout(databaseOptions.CommandTimeout);
                });
            });

            return services;
        }
    }
}
