using EcleticaBeerControl.Application.Members.Commands;
using EcleticaBeerControl.Domain.Primitives;
using MediatR.Pipeline;

namespace EcleticaBeerControl.Application.Processors
{
    internal sealed class BaseCommandMetadataPreProcessor<TRequest> 
        : IRequestPreProcessor<TRequest>
        where TRequest : BaseCommand

    {
        private readonly BreweryUser _brewer;

        public BaseCommandMetadataPreProcessor(BreweryUser brewer) {
            _brewer = brewer;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            request.CreateBy = _brewer.Id;
            request.CreatedAt = DateTime.UtcNow;

            return Task.CompletedTask;
        }
    }
}
