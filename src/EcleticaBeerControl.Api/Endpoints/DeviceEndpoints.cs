using EcleticaBeerControl.Application.Members.Commands.Devices;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EcleticaBeerControl.Api.Endpoints
{
    public static class DeviceEndpoints
    {
        public static void MapDeviceEndpoints(this IEndpointRouteBuilder app)
        {
            var devices = app.MapGroup("devices")
                            .RequireAuthorization();

            devices.MapPost("/", CreateDevice);
        }

        private static async Task<Results<Ok<string>, BadRequest<string[]>>> CreateDevice([FromBody] CreateDeviceCommand command, ISender sender)
        {
            var response = await sender.Send(command);

            if (response.IsSuccess)
                return TypedResults.Ok(response.Value);

            return TypedResults.BadRequest(response.Errors);
        }
    }
}