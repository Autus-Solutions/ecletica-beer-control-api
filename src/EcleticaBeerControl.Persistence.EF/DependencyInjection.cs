using EcleticaBeerControl.Persistence.EF.Database;
using EcleticaBeerControl.Persistence.EF.Database.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EcleticaBeerControl.Persistence.EF
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEFPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<DatabaseOptionsSetup>();

            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var databaseOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>()!.Value;

                options.UseNpgsql(databaseOptions.ConnectionString, options =>
                {
                    options.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    options.CommandTimeout(databaseOptions.CommandTimeout);
                })
                .UseSnakeCaseNamingConvention();

                options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            });

            return services;
        }
    }
}
