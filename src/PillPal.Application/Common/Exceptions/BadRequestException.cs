namespace PillPal.Application.Common.Exceptions;

public class BadRequestException(string message)
    : HttpException(message, HttpStatusCode.BadRequest)
{
}
