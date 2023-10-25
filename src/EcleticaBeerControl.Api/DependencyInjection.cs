using EcleticaBeerControl.Api.Middlewares;
using EcleticaBeerControl.Domain.Primitives;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RabbitMQ.Client.Core.DependencyInjection;
using System.Text;
using EcleticaBeerControl.Infrastructure.Messaging.RabbitMq;

namespace EcleticaBeerControl.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            #region API Authentication/Authorization

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

            #endregion

            #region HttpContext

            services.AddHttpContextAccessor();
            services.AddScoped((provider) =>
            {
                var context = provider.GetRequiredService<IHttpContextAccessor>();

                return new BreweryUser
                {
                    Id = Guid.Parse(context.HttpContext!.Items["id"]!.ToString()!),
                    BreweryId = Guid.Parse(context.HttpContext!.Items["brewery_id"]!.ToString()!),
                    BreweryName = context.HttpContext!.Items["brewery_name"]!.ToString()!,
                    BreweryRegistred = bool.Parse(context.HttpContext!.Items["brewery_registred"]!.ToString()!),
                    Owner = bool.Parse(context.HttpContext!.Items["owner"]!.ToString()!),
                };
            });

            #endregion

            #region Middlewares

            services.AddScoped<GlobalErrorHandlingMiddleware>();
            services.AddScoped<SupabaseAuthMiddleware>();

            #endregion

            services.ConfigureRabbitMqTopology();

            return services;
        }

        private static IServiceCollection ConfigureRabbitMqTopology(this IServiceCollection services)
        {
            services.AddProductionExchange("ebc.devices", RabbitMqConfiguration.Topology);
            return services;
        }
    }
}
