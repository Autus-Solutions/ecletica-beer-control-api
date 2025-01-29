using EcleticaBeerControl.Application.Members.Commands.Breweries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EcleticaBeerControl.Api.Controllers
{
    public class BreweryEndpoints
    {
        public static void AddRoutes(IEndpointRouteBuilder app)
        {
            var brewery = app.MapGroup("brewery")
                            .RequireAuthorization();

            brewery.MapPost("/", RegisterBrewery);
        }

        private static async Task<Results<Ok<string>, BadRequest<string[]>>> RegisterBrewery(RegisterBreweryIfNeededCommand command, ISender sender)
        {
            var response = await sender.Send(command);

            if (response.IsSuccess)
                return TypedResults.Ok(response.Value);

            return TypedResults.BadRequest(response.Errors);
        }
    }
}