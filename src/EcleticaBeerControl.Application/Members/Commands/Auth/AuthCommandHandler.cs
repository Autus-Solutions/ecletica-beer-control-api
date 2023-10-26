using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Primitives;
using EcleticaBeerControl.Domain.Repositories;
using MediatR;

namespace EcleticaBeerControl.Application.Members.Commands.Auth
{
    public sealed class AuthCommandHandler
        : BaseCommandHandler,
            IRequestHandler<RegisterUserCommand, Result<Guid>>,
            IRequestHandler<RegisterBreweryIfNeededCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBreweryRepository _breweryRepository;

        public AuthCommandHandler(IUserRepository userRepository, IBreweryRepository breweryRepository, IPublisher publisher) : base(publisher)
        {
            _userRepository = userRepository;
            _breweryRepository = breweryRepository;
        }

        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = User.Register(
                request.Id,
                request.Name,
                request.CreateBy);

                await _userRepository.Insert(user, cancellationToken);
                await PublishEvents(user, cancellationToken);

                return Result<Guid>.Success(user.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }
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
