using EcleticaBeerControl.Domain.Interfaces.Services;

namespace EcleticaBeerControl.Api.Middlewares
{
    public class BreweryResolverMiddleware(IBreweryResolver breweryResolver) : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var currentIdentity = context.User?.Identity;

            if (currentIdentity?.IsAuthenticated ?? false)
                await breweryResolver.SetBrewery(currentIdentity?.Name);

            await next(context);
        }
    }

    public static class BreweryResolverMiddlewareExtensions
    {
        public static IApplicationBuilder UseBreweryResolver(this IApplicationBuilder app)
        {
            return app.UseMiddleware<BreweryResolverMiddleware>();
        }

    }
}