using EcleticaBeerControl.Application.Members.Commands.Devices;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EcleticaBeerControl.Api.Endpoints
{
    public static class DeviceEndpoints
    {
        public static void MapDeviceEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("devices")
                            .RequireAuthorization();

            group.MapPost("/", CreateDevice);
        }

        private static async Task<Results<Ok<string>, BadRequest<string[]>>> CreateDevice(CreateDeviceCommand command, ISender sender)
        {
            var response = await sender.Send(command);

            if (response.IsSuccess)
                return TypedResults.Ok(response.Value);

            return TypedResults.BadRequest(response.Errors);
        }
    }
}