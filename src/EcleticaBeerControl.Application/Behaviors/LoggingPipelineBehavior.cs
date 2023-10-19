using EcleticaBeerControl.Domain.Primitives;
using MediatR;
using Serilog;

namespace EcleticaBeerControl.Application.Behaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly ILogger _logger;

        public LoggingPipelineBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.Information("Starting Request {@RequestName}, {@DateTimeUtc}", 
                                typeof(TRequest).Name,
                                DateTime.UtcNow);

            var result = await next();

            if (result.IsFailure)
            {
                _logger.Error("Request Failure {@RequestName}, {@Error}, {@DateTimeUtc}",
                    typeof(TRequest).Name,
                    result.Error,
                    DateTime.UtcNow);
            }


            _logger.Information("Completed Request {@RequestName}, {@DateTimeUtc}",
                                typeof(TRequest).Name,
                                DateTime.UtcNow);

            return result;
        }
    }
}
