using Supabase;
using System.Net.Http.Headers;
using System.Net;
using ILogger = Serilog.ILogger;

namespace EcleticaBeerControl.Api.Middlewares
{
    public class SupabaseAuthMiddleware : IMiddleware
    {
        private readonly Client _client;
        private readonly ILogger _logger;

        public SupabaseAuthMiddleware(Client client, ILogger logger) { 
            _client = client;
            _logger = logger;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (AuthenticationHeaderValue.TryParse(context.Request.Headers.Authorization, out var headerValue))
            {
                _client.Auth.SetAuth(headerValue.Parameter ?? string.Empty);
            }

            return next.Invoke(context);
        }
    }

    public static class SupabaseAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseSupabaseAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SupabaseAuthMiddleware>();
        }

    }
}
