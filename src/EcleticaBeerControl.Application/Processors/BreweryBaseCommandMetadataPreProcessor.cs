using EcleticaBeerControl.Application.Members.Commands;
using EcleticaBeerControl.Domain.Primitives;
using MediatR.Pipeline;

namespace EcleticaBeerControl.Application.Processors
{
    public sealed class BreweryBaseCommandMetadataPreProcessor<TRequest>
        : IRequestPreProcessor<TRequest>
        where TRequest : BaseBreweryCommand
    {
        private readonly BreweryUserContext _brewer;

        public BreweryBaseCommandMetadataPreProcessor(BreweryUserContext brewer)
        {
            _brewer = brewer;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            request.BreweryId = _brewer.BreweryId;
            return Task.CompletedTask;
        }
    }
}
