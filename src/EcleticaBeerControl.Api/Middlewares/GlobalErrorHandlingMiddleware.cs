using EcleticaBeerControl.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EcleticaBeerControl.Api.Middlewares
{
    public class GlobalErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                Log.Error(e, "Exception occurred: {Message}", e.Message);

                var exeptionDetails = GetExceptionDetails(e);

                ProblemDetails problemDetails = new()
                {
                    Status = exeptionDetails.Status,
                    Type = exeptionDetails.Type,
                    Title = exeptionDetails.Title,
                    Detail = exeptionDetails.Detail
                };

                if (exeptionDetails.Errors is not null)
                    problemDetails.Extensions[nameof(ExceptionDetails.Errors)] = exeptionDetails.Errors;

                context.Response.StatusCode = exeptionDetails.Status;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }

        private static ExceptionDetails GetExceptionDetails(Exception exception)
        {
            return exception switch
            {
                DomainValidationException validationException => new ExceptionDetails(
                    StatusCodes.Status400BadRequest,
                    "ValidationFailure",
                    "Validation error",
                    "One or more validation erros has occurred",
                    validationException.Errors),
                _ => new ExceptionDetails(
                    StatusCodes.Status500InternalServerError,
                    "ServerError",
                    "Server error",
                    "An unexpected error has occurred",
                    null)
            };
        }

        internal record ExceptionDetails(
            int Status,
            string Type,
            string Title,
            string Detail,
            IEnumerable<object>? Errors);
    }

    public static class GlobalErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalErrorHandlingMiddleware>();
        }

    }
}
