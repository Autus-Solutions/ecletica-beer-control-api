using EcleticaBeerControl.Application.Members.Commands.Breweries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EcleticaBeerControl.Api.Endpoints
{
    public static class BreweryEndpoints
    {
        public static void MapBreweryEndpoints(this IEndpointRouteBuilder app)
        {
            var breweries = app.MapGroup("brewery")
                            .RequireAuthorization();

            breweries.MapPost("/", RegisterBrewery);
        }

        private static async Task<Results<Ok<string>, BadRequest<string[]>>> RegisterBrewery([FromBody] RegisterBreweryIfNeededCommand command, ISender sender)
        {
            var response = await sender.Send(command);

            if (response.IsSuccess)
                return TypedResults.Ok(response.Value);

            return TypedResults.BadRequest(response.Errors);
        }
    }
}