using EcleticaBeerControl.Application.Members.Commands;
using EcleticaBeerControl.Domain.Interfaces;
using MediatR.Pipeline;

namespace EcleticaBeerControl.Application.Processors
{
    public sealed class BaseCommandMetadataPreProcessor<TRequest>
        : IRequestPreProcessor<TRequest>
        where TRequest : BaseCommand

    {
        private readonly IUser _user;

        public BaseCommandMetadataPreProcessor(IUser user)
        {
            _user = user;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            request.CreateBy = _user.Id;
            return Task.CompletedTask;
        }
    }
}
