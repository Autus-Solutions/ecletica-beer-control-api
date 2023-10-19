using Supabase;
using System.Net.Http.Headers;

namespace EcleticaBeerControl.Api.Middlewares
{
    public class SupabaseAuthMiddleware : IMiddleware
    {
        private readonly Client _client;

        public SupabaseAuthMiddleware(Client client) { 
            _client = client;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (AuthenticationHeaderValue.TryParse(context.Request.Headers.Authorization, out var headerValue))
            {
                var acessToken = headerValue.Parameter ?? string.Empty;
                var refreshToken = context.Request.Headers["X-Supabase-RefreshToken"].ToString() ?? string.Empty;

                await _client.Auth.SetSession(acessToken, refreshToken);

                var userId = Guid.Parse(_client.Auth.CurrentUser!.Id);
                var userClientId = Guid.Parse(_client.Auth.CurrentUser!.UserMetadata["client_id"].ToString());

                context.Items.Add(nameof(userId), userId);
                context.Items.Add(nameof(userClientId), userClientId);
            }

            await next.Invoke(context);
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
