using Carter;
using EcleticaBeerControl.Application.Members.Commands.Devices;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EcleticaBeerControl.Api.Controllers
{
    public class DeviceEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/devices")
                            .RequireAuthorization();

            group.MapPost("", CreateDevice)
                    .WithName(nameof(CreateDevice));
        }

        public static async Task<Results<Ok,BadRequest>> CreateDevice(CreateDeviceCommand command, ISender sender)
        {
            await sender.Send(command);
            return TypedResults.Ok();
        }

    }
}