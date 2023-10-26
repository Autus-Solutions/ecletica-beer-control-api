using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Primitives;
using EcleticaBeerControl.Domain.Repositories;
using MediatR;

namespace EcleticaBeerControl.Application.Members.Commands.Auth
{
    public sealed class AuthCommandHandler
        : BaseCommandHandler,
            IRequestHandler<RegisterBreweryIfNeededCommand, Result<Guid>>
    {
        private readonly IBreweryRepository _breweryRepository;

        public AuthCommandHandler(IBreweryRepository breweryRepository, IPublisher publisher) : base(publisher)
        {
            _breweryRepository = breweryRepository;
        }

        public async Task<Result<Guid>> Handle(RegisterBreweryIfNeededCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var brewery = Brewery.Register(
                request.Id,
                request.Name,
                request.CreateBy);

                await _breweryRepository.Insert(brewery, cancellationToken);
                await PublishEvents(brewery, cancellationToken);

                return Result<Guid>.Success(brewery.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }
        }

    }
}
