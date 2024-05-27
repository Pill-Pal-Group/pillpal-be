using FluentValidation.Results;

namespace PillPal.Application.Common.Exceptions;

public class ValidationException : HttpException
{
    public ValidationException()
        : base("One or more validation failures have occurred.",
            HttpStatusCode.UnprocessableEntity)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
