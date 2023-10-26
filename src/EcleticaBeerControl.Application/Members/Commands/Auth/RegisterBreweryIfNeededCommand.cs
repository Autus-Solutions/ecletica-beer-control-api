using EcleticaBeerControl.Domain.Primitives;
using MediatR;

namespace EcleticaBeerControl.Application.Members.Commands.Auth
{
    public record RegisterBreweryIfNeededCommand : BaseCommand, IRequest<Result<Guid>>
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
    }
}
