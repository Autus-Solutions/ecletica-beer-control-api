using EcleticaBeerControl.Domain.Primitives;
using MediatR;

namespace EcleticaBeerControl.Application.Members.Commands.Devices
{
    public record CreateDeviceCommand : BaseClientCommand, IRequest<Result>
    {
        public string Identifier { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
