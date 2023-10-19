using EcleticaBeerControl.Application.Members.Commands;
using EcleticaBeerControl.Domain.Primitives;
using MediatR.Pipeline;

namespace EcleticaBeerControl.Application.Processors
{
    public class BaseDomainMetadataPreProcessor<TRequest> 
        : IRequestPreProcessor<TRequest>
        where TRequest : BaseClientCommand

    {
        private readonly User _user;

        public BaseDomainMetadataPreProcessor(User user) {
            _user = user;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            request.CreateBy = _user.Id;
            request.CreatedAt = DateTime.UtcNow;

            return Task.CompletedTask;
        }
    }
}
