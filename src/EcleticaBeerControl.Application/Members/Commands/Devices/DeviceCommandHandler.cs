using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Primitives;
using EcleticaBeerControl.Domain.Repositories;
using MediatR;

namespace EcleticaBeerControl.Application.Members.Commands.Devices
{
    internal sealed class DeviceCommandHandler 
        : BaseCommandHandler,
          IRequestHandler<CreateDeviceCommand, Result>
    {
        private readonly IDeviceRepository _repository;

        public DeviceCommandHandler(IDeviceRepository repository, IPublisher publisher) : base(publisher) 
        {
            _repository = repository;
        }

        public async Task<Result> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = Device.Create(
                    request.ClientId,
                    request.Name,
                    request.Description,
                    request.CreateBy);

            await _repository.Insert(device, cancellationToken);
            await PublishEvents(device, cancellationToken);

            return new Result();
        }
    }
}
