using EcleticaBeerControl.Domain.Primitives;
using MediatR;

namespace EcleticaBeerControl.Application.Members.Commands.Devices
{
    public record CreateDeviceCommand : BaseBreweryCommand, IRequest<Result<Guid>>
    {
        public required string Identifier { get; init; }
        public required string Name { get; init; }
        public string? Description { get; init; }
    }
}
