using EcleticaBeerControl.Application.Members.Commands.Devices;
using EcleticaBeerControl.Application.Resources;
using FluentValidation;

namespace EcleticaBeerControl.Application.Members.Validators.Devices
{
    public class CreateDeviceValidator : AbstractValidator<CreateDeviceCommand>
    {
        public CreateDeviceValidator()
        {

            RuleFor(d => d.Identifier)
                    .NotEmpty()
                    .WithMessage(Messages.DeviceIdentifierCannotBeNullOrEmpty);

            RuleFor(d => d.Name)
                    .NotEmpty()
                    .WithMessage(Messages.DeviceNameCannotBeNullOrEmpty);
        }
    }
}
