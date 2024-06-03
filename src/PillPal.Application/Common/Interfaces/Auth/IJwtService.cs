using PillPal.Core.Identity;
using System.Security.Claims;

namespace PillPal.Application.Common.Interfaces.Auth;

public interface IJwtService
{
    public (string accessToken, int expired) GenerateJwtToken(ApplicationUser user, string role);
    public string GetEmailPrincipal(string token);
    public string GenerateRefreshToken(string token);
    public bool ValidateRefreshToken(string token, string refreshToken);
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
