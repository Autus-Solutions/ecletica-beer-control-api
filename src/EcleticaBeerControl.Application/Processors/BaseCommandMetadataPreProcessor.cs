using EcleticaBeerControl.Application.Members.Commands;
using EcleticaBeerControl.Domain.Contexts;
using MediatR.Pipeline;

namespace EcleticaBeerControl.Application.Processors
{
    public sealed class BaseCommandMetadataPreProcessor<TRequest>
        : IRequestPreProcessor<TRequest>
        where TRequest : BaseCommand

    {
        private readonly BreweryUserContext _brewer;

        public BaseCommandMetadataPreProcessor(BreweryUserContext brewer)
        {
            _brewer = brewer;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            request.CreateBy = _brewer.Id;
            return Task.CompletedTask;
        }
    }
}
