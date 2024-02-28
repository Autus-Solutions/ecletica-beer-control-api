using EcleticaBeerControl.Application.Members.Commands;
using EcleticaBeerControl.Domain.Interfaces;
using MediatR.Pipeline;

namespace EcleticaBeerControl.Application.Processors
{
    public sealed class BreweryBaseCommandMetadataPreProcessor<TRequest>
        : IRequestPreProcessor<TRequest>
        where TRequest : BaseBreweryCommand
    {
        private readonly IUser _user;

        public BreweryBaseCommandMetadataPreProcessor(IUser user)
        {
            _user = user;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            request.BreweryId = _user.BreweryId;
            return Task.CompletedTask;
        }
    }
}
