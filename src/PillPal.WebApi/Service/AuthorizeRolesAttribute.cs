using Microsoft.AspNetCore.Authorization;

namespace PillPal.WebApi.Service;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params string[] roles)
        => Roles = string.Join(",", roles);
}
