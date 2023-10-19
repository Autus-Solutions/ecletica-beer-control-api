using EcleticaBeerControl.Api.Middlewares;
using EcleticaBeerControl.Domain.Primitives;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace EcleticaBeerControl.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

                        // Presisting enums as text
                        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("SupabaseProjectJwtSecret") ?? string.Empty)),
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });

            services.AddHttpContextAccessor();

            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            services.AddSingleton<Serilog.ILogger>(log);

            services.AddScoped<GlobalErrorHandlingMiddleware>();
            services.AddScoped<SupabaseAuthMiddleware>();

            services.AddScoped<User>((provider) =>
            {
                var context = provider.GetRequiredService<IHttpContextAccessor>();

                var userId = Guid.Parse(context.HttpContext.Items["userId"].ToString());
                var userClientId = Guid.Parse(context.HttpContext.Items["userClientId"].ToString());

                return new User
                {
                    Id = userId,
                    ClientId = userClientId
                };
            });

            return services;
        }
    }
}
