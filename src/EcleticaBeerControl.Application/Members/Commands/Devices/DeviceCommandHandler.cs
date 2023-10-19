using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Primitives;
using EcleticaBeerControl.Domain.Repositories;
using MediatR;

namespace EcleticaBeerControl.Application.Members.Commands.Devices
{
    public class DeviceCommandHandler : IRequestHandler<CreateDeviceCommand, Result>
    {
        private readonly IDeviceRepository _repository;

        public DeviceCommandHandler(IDeviceRepository repository)
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

            return new Result();
        }
    }
}
