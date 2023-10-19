namespace EcleticaBeerControl.Domain.Primitives
{
    public record Result
    {
        public bool IsFailure { get; init; }
        public string? Error { get; init; }
    }
}
