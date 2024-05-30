using System.Security.Claims;
using PillPal.Core.Identity;

namespace PillPal.Application.Common.Interfaces.Auth;

public interface IJwtService
{
    public string GenerateJwtToken(ApplicationUser user, string role);
    public ClaimsPrincipal GetPrincipalFromToken(string token);
    public string GenerateRefreshToken(string token);
    public string GetUserEmailFromToken(string token);
    public bool ValidateFirebaseToken(string token);
}
