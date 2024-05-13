using System.Security.Claims;

namespace PillPal.WebApi.Service;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _accessor;

    public UserAccessor(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string UserId => _accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
}

public interface IUserAccessor
{
    public string UserId { get; }
}
