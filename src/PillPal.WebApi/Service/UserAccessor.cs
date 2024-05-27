using PillPal.Application.Common.Interfaces.Data;
using System.Security.Claims;

namespace PillPal.WebApi.Service;

public class UserAccessor(IHttpContextAccessor accessor) : IUser
{
    public string? Id =>
        accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Manual";
}
