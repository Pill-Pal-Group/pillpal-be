namespace PillPal.Application.Common.Exceptions;

public class NotFoundException(string message)
    : HttpException(message, HttpStatusCode.NotFound)
{
}
