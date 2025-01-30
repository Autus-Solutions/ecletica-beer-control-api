using EcleticaBeerControl.Api.Middlewares;
using RabbitMQ.Client.Core.DependencyInjection;
using EcleticaBeerControl.Infrastructure.Messaging.RabbitMq;
using FluentValidation;
using EcleticaBeerControl.Application;
using EcleticaBeerControl.Application.Processors;
using EcleticaBeerControl.Application.Behaviors;
using EcleticaBeerControl.Domain.Models;
using EcleticaBeerControl.Persistence.EF.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Newtonsoft.Json;
using EcleticaBeerControl.Application.Members.Services;
using EcleticaBeerControl.Domain.Interfaces.Services;
using Asp.Versioning;
using EcleticaBeerControl.Api.OpenApi;

namespace EcleticaBeerControl.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IBreweryResolver, BreweryResolver>();

            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
                    });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes;
            });

            services.AddHttpContextAccessor()
                    .AddApiMiddlewares()
                    .AddApiVersioning()
                    .AddApiIdentitySecurity()
                    .AddMediatR()
                    .AddFluentValidator()
                    .AddRabbitMq();

            return services;
        }

        private static IServiceCollection AddApiMiddlewares(this IServiceCollection services)
        {
            services.AddScoped<BreweryResolverMiddleware>();
            services.AddScoped<GlobalErrorHandlingMiddleware>();
            return services;
        }
        private static IServiceCollection AddApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            services.ConfigureOptions<ConfigureSwaggerGenOptions>();

            return services;
        }

        private static IServiceCollection AddApiIdentitySecurity(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthorization();
            services.AddAuthentication();

            services.AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddApiEndpoints();

            return services;
        }

        private static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR((configuration) =>
            {
                configuration.Lifetime = ServiceLifetime.Scoped;
                configuration.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
                configuration.AddOpenBehavior(typeof(FailFastValidationBehavior<,>), ServiceLifetime.Scoped);
                configuration.AddOpenRequestPreProcessor(typeof(BaseCommandMetadataPreProcessor<>), ServiceLifetime.Scoped);
                configuration.AddOpenRequestPreProcessor(typeof(BreweryBaseCommandMetadataPreProcessor<>), ServiceLifetime.Scoped);
            });
            return services;
        }
        private static IServiceCollection AddFluentValidator(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
            return services;
        }
        private static IServiceCollection AddRabbitMq(this IServiceCollection services)
        {
            services.AddProductionExchange("ebc.devices", RabbitMqConfiguration.Topology);
            return services;
        }
    }
}
