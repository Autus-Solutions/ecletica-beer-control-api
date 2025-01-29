using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Primitives;
using MediatR;

namespace EcleticaBeerControl.Application.Members.Commands.Breweries
{
    internal sealed class BreweryCommandHandler
        : BaseCommandHandler,
            IRequestHandler<RegisterBreweryIfNeededCommand, Result<string>>
    {
        //private readonly IBreweryRepository _breweryRepository;

        public BreweryCommandHandler(/*IBreweryRepository breweryRepository,*/ IPublisher publisher) : base(publisher)
        {
            //_breweryRepository = breweryRepository;
        }

        public async Task<Result<string>> Handle(RegisterBreweryIfNeededCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var brewery = Brewery.Register(
                request.Id,
                request.Name,
                request.CreateBy);

                //await _breweryRepository.Insert(brewery, cancellationToken);
                await PublishEvents(brewery, cancellationToken);

                return Result<string>.Success(brewery.Id);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(ex.Message);
            }
        }

    }
}
