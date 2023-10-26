namespace EcleticaBeerControl.Domain.Primitives
{
    public record Result<T> : Result
    {
        public T? Value { get; private set; }

        public static Result<T> Success(T value) => new() { IsSuccess = true, Value = value };
        public static Result<T> Failure(params string[] errors) => new() { IsSuccess = false, Errors = errors };
    }

    public abstract record Result
    {
        public bool IsSuccess { get; protected set; }
        public string[] Errors { get; protected set; } = Enumerable.Empty<string>().ToArray();

    }
}
