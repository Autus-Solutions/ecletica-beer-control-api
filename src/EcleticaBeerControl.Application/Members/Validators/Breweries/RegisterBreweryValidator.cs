using EcleticaBeerControl.Application.Members.Commands.Auth;
using EcleticaBeerControl.Application.Resources;
using FluentValidation;

namespace EcleticaBeerControl.Application.Members.Validators.Breweries
{
    public class RegisterBreweryValidator : AbstractValidator<RegisterBreweryIfNeededCommand>
    {
        public RegisterBreweryValidator()
        {
            RuleFor(d => d.Id)
                   .NotEmpty()
                   .WithMessage(Messages.BreweryIdCannotBeNullOrEmpty);

            RuleFor(d => d.Name)
                .NotEmpty()
                .WithMessage(Messages.BreweryNameCannotBeNullOrEmpty);
        }
    }
}
