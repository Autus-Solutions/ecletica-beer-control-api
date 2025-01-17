using Carter;
using EcleticaBeerControl.Application.Members.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EcleticaBeerControl.Api.Controllers
{
    public class AuthEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/auth")
                            .RequireAuthorization();

            group.MapPost("/brewery/register", RegisterBrewery)
                    .WithName(nameof(RegisterBrewery));
        }

        public static async Task<Results<Ok<Guid>, BadRequest<string[]>>> RegisterBrewery(RegisterBreweryIfNeededCommand command, ISender sender)
        {
            var response = await sender.Send(command);

            if (response.IsSuccess)
                return TypedResults.Ok(response.Value);

            return TypedResults.BadRequest(response.Errors);
        }
    }
}