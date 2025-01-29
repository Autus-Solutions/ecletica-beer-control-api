using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Primitives;
using MediatR;

namespace EcleticaBeerControl.Application.Members.Commands.Devices
{
    internal sealed class DeviceCommandHandler
        : BaseCommandHandler,
          IRequestHandler<CreateDeviceCommand, Result<string>>
    {
        //private readonly IDeviceRepository _repository;

        public DeviceCommandHandler(/*IDeviceRepository repository, */ IPublisher publisher) : base(publisher)
        {
            //_repository = repository;
        }

        public async Task<Result<string>> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var device = Device.Create(
                request.BreweryId,
                request.Identifier,
                request.Name,
                request.Description,
                request.CreateBy);

                //await _repository.Insert(device, cancellationToken);
                await PublishEvents(device, cancellationToken);

                return Result<string>.Success(device.Id);

            }
            catch (Exception ex)
            {
                return Result<string>.Failure(ex.Message);
            }
        }
    }
}
