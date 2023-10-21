using EcleticaBeerControl.Domain.Primitives;
using MediatR;
using Serilog;

namespace EcleticaBeerControl.Application.Behaviors
{
    internal sealed class LoggingPipelineBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Log.Information("Starting Request {RequestName}, {DateTimeUtc}", 
                                typeof(TRequest).Name,
                                DateTime.UtcNow);

            var result = await next();

            if (!result.IsSuccess)
            {
                Log.Error("Request Failure {RequestName}, {Error}, {DateTimeUtc}",
                    typeof(TRequest).Name,
                    result.Errors,
                    DateTime.UtcNow);
            }


            Log.Information("Completed Request {RequestName}, {DateTimeUtc}",
                                typeof(TRequest).Name,
                                DateTime.UtcNow);

            return result;
        }
    }
}
