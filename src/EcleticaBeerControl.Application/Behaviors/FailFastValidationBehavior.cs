using EcleticaBeerControl.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace EcleticaBeerControl.Application.Behaviors
{
    public sealed class FailFastValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public FailFastValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            var validationContext = new ValidationContext<TRequest>(request);

            var validationFailures = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(validationContext)));

            var errors = validationFailures
                .Where(vr => !vr.IsValid)
                .SelectMany(vr => vr.Errors)
                .Select(vf => new ValidationError(vf.PropertyName, vf.ErrorMessage))
                .ToList();

            if (errors.Any())
            {

                throw new DomainValidationException(errors);
            }

            return await next();
        }
    }
}
