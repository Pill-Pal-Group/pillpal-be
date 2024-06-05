namespace PillPal.Application.Common.Exceptions;

public class NotFoundException(string entityName, object key)
    : HttpException($"Entity '{entityName}' with identifier ({key}) was not found.", HttpStatusCode.NotFound)
{
}
