using EcleticaBeerControl.Api.Middlewares;
using EcleticaBeerControl.Domain.Primitives;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RabbitMQ.Client.Core.DependencyInjection;
using System.Text;
using EcleticaBeerControl.Infrastructure.Messaging.RabbitMq;
using FluentValidation;
using EcleticaBeerControl.Application;
using EcleticaBeerControl.Application.Processors;
using EcleticaBeerControl.Application.Behaviors;
using Supabase.Gotrue;

namespace EcleticaBeerControl.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiMiddlewares()
                    .AddApiAuthentication(configuration)
                    .AddHttpContextAccessor()
                    .AddBreweryUserContext()
                    .AddSupabaseAuth()
                    .AddMediatR()
                    .AddFluentValidator()
                    .AddRabbitMq();

            return services;
        }

        private static IServiceCollection AddApiMiddlewares(this IServiceCollection services)
        {

            services.AddScoped<GlobalErrorHandlingMiddleware>();
            services.AddScoped<SupabaseAuthMiddleware>();

            return services;
        }
        private static IServiceCollection AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
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

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.IncludeErrorDetails = true;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = "authenticated",
                    ValidIssuer = configuration.GetValue<string>("SupabaseProjectUrl"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("SupabaseProjectJwtSecretKey") ?? string.Empty)),
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });

            return services;
        }
        private static IServiceCollection AddSupabaseAuth(this IServiceCollection services)
        {
            services.AddScoped<Client>();
            return services;
        }
        private static IServiceCollection AddBreweryUserContext(this IServiceCollection services)
        {
            services.AddScoped((provider) =>
            {
                var context = provider.GetRequiredService<IHttpContextAccessor>();

                return new BreweryUserContext
                {
                    Id = Guid.Parse(context.HttpContext!.Items["id"]!.ToString()!),
                    BreweryId = Guid.Parse(context.HttpContext!.Items["brewery_id"]!.ToString()!),
                    BreweryName = context.HttpContext!.Items["brewery_name"]!.ToString()!,
                    BreweryRegistred = bool.Parse(context.HttpContext!.Items["brewery_registred"]!.ToString()!),
                    Owner = bool.Parse(context.HttpContext!.Items["owner"]!.ToString()!),
                };
            });
            return services;
        }
        private static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR((configuration) =>
            {
                configuration.Lifetime = ServiceLifetime.Scoped;
                configuration.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
                configuration.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>), ServiceLifetime.Scoped);
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
