using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Primitives;
using EcleticaBeerControl.Domain.Repositories;
using MediatR;

namespace EcleticaBeerControl.Application.Members.Commands.Breweries
{
    internal sealed class BreweryCommandHandler
        : BaseCommandHandler,
          IRequestHandler<RegisterBreweryIfNeededCommand, Result<Guid>>
    {
        private readonly IBreweryRepository _repository;

        public BreweryCommandHandler(IBreweryRepository repository, IPublisher publisher) : base(publisher)
        {
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(RegisterBreweryIfNeededCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var client = Brewery.Register(
                request.Id,
                request.Name,
                request.CreateBy);

                await _repository.Insert(client, cancellationToken);
                await PublishEvents(client, cancellationToken);

                return Result<Guid>.Success(client.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }
        }
    }
}
