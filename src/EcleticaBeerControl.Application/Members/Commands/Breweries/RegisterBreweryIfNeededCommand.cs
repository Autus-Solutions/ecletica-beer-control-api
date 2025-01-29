using EcleticaBeerControl.Domain.Primitives;
using MediatR;

namespace EcleticaBeerControl.Application.Members.Commands.Breweries
{
    public record RegisterBreweryIfNeededCommand : BaseCommand, IRequest<Result<string>>
    {
        public required string Id { get; init; }
        public required string Name { get; init; }
    }
}
