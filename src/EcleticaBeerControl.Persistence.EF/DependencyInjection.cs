using EcleticaBeerControl.Persistence.EF.Database;
using EcleticaBeerControl.Persistence.EF.Database.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace EcleticaBeerControl.Persistence.EF
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEFPersistence(this IServiceCollection services, IHostEnvironment environment)
        {
            services.ConfigureOptions<DatabaseOptionsSetup>();

            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var databaseOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                options.UseNpgsql(databaseOptions.ConnectionString, options =>
                {
                    options.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    options.CommandTimeout(databaseOptions.CommandTimeout);
                });

                if (environment.IsDevelopment())
                {
                    options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                    options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
                }
            });

            services.AddDbContext<IdentityDbContext>((provider, options) =>
            {
                var databaseOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                options.UseNpgsql(databaseOptions.ConnectionString, options =>
                {
                    options.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    options.CommandTimeout(databaseOptions.CommandTimeout);
                });

                if (environment.IsDevelopment())
                {
                    options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                    options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
                }

            });

            return services;
        }
    }
}
