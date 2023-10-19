using EcleticaBeerControl.Application.Members.Commands.Devices;
using FluentValidation;

namespace EcleticaBeerControl.Application.Validators.Devices
{
    public class CreateDeviceValidator : AbstractValidator<CreateDeviceCommand>
    {
        public CreateDeviceValidator() { 

            RuleFor(d => d.Name).NotEmpty().WithMessage("O nome do dispositivo não pode ser vazio");
        }
    }
}
