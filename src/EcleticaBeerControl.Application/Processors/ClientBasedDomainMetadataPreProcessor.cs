using EcleticaBeerControl.Application.Members.Commands;
using EcleticaBeerControl.Domain.Primitives;
using MediatR.Pipeline;

namespace EcleticaBeerControl.Application.Processors
{
    public class ClientBasedDomainMetadataPreProcessor<TRequest> 
        : IRequestPreProcessor<TRequest>
        where TRequest : BaseClientCommand

    {
        private readonly User _user;

        public ClientBasedDomainMetadataPreProcessor(User user) {
            _user = user;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            request.ClientId = _user.ClientId;
            return Task.CompletedTask;
        }
    }
}
