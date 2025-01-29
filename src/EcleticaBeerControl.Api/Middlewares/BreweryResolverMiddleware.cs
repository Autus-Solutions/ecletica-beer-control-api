using EcleticaBeerControl.Application.Members.Services;
using EcleticaBeerControl.Domain.Interfaces.Services;

namespace EcleticaBeerControl.Api.Middlewares
{
    public class BreweryResolverMiddleware(IBreweryService breweryService) : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            breweryService.SetBrewery("teste");
            await next(context);
        }
    }
}
