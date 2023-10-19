using EcleticaBeerControl.Application.Members.Commands.Devices;
using FluentValidation;

namespace EcleticaBeerControl.Application.Validators.Devices
{
    public class CreateDeviceValidator : AbstractValidator<CreateDeviceCommand>
    {
        public CreateDeviceValidator() {

            RuleFor(d => d.Identifier).NotNull()
                                      .WithMessage("O identificador do dispositivo não pode ser vazio.")
                                      .NotEmpty()
                                      .WithMessage("O identificador do dispositivo não pode ser vazio.");

            RuleFor(d => d.Name).NotNull()
                                .WithMessage("O nome do dispositivo não pode ser vazio.")
                                .NotEmpty()
                                .WithMessage("O nome do dispositivo não pode ser vazio.");
        }
    }
}
