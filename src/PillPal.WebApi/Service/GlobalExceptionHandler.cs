using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Exceptions;
using ValidationException = PillPal.Application.Common.Exceptions.ValidationException;

namespace PillPal.WebApi.Service;

public class GlobalExceptionHandler : IExceptionHandler
{
    private static readonly string[] _separator = ["\r\n", "\r", "\n"];

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var statusCode = exception switch
        {
            HttpException httpException => (int)httpException.StatusCode,
            _ => StatusCodes.Status500InternalServerError
        };

        var problemDetails = new ProblemDetails
        {
            Type = exception.GetType().Name,
            Title = "An error occurred while processing your request.",
            Status = statusCode,
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("errors", validationException.Errors);
        }

        if (httpContext.RequestServices.GetRequiredService<IHostEnvironment>().IsDevelopment() ||
            httpContext.RequestServices.GetRequiredService<IHostEnvironment>().IsStaging())
        {
            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
            problemDetails.Extensions.Add("stackTrace", exception.StackTrace?.Split(_separator, StringSplitOptions.None));
        }

        httpContext.Response.ContentType = "application/problem+json";
        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
