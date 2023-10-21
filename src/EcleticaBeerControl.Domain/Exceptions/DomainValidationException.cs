namespace EcleticaBeerControl.Domain.Exceptions
{
    public class DomainValidationException : Exception
    {
        public IReadOnlyCollection<ValidationError> Errors { get; private set; }

        public DomainValidationException(IReadOnlyCollection<ValidationError> errors) : base("Validation failed !")
        {
            Errors = errors;
        }
    }

    public record ValidationError(string PropertyName, string ErrorMessage);
}
