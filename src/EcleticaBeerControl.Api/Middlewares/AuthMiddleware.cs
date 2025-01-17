using System.Net.Http.Headers;

namespace EcleticaBeerControl.Api.Middlewares
{
    public class AuthMiddleware : IMiddleware
    {

        public AuthMiddleware()
        {

        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (AuthenticationHeaderValue.TryParse(context.Request.Headers.Authorization, out var headerValue))
            {
                var acessToken = headerValue.Parameter ?? string.Empty;
                //var refreshToken = context.Request.Headers["X-Supabase-RefreshToken"].ToString() ?? string.Empty;

                //var client = await _client.InitializeAsync();
                //await client.Auth.SetSession(acessToken, refreshToken);

                //var user = client.Auth.CurrentUser!;

                //context.Items.Add("id", Guid.Parse(user.Id!));
                //context.Items.Add("brewery_id", Guid.Parse(user.UserMetadata["brewery_id"]!.ToString()!));
                //context.Items.Add("brewery_name", Guid.Parse(user.UserMetadata["brewery_name"]!.ToString()!));
                //context.Items.Add("brewery_registred", bool.Parse(user.UserMetadata["brewery_registred"]!.ToString()!));
                //context.Items.Add("owner", bool.Parse(user.UserMetadata["owner"]!.ToString()!));
            }

            await next.Invoke(context);
        }
    }
    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthMiddleware>();
        }

    }
}
