using PillPal.Core.Identity;
using System.Security.Claims;

namespace PillPal.Application.Common.Interfaces.Auth;

public interface IJwtService
{
    /// <summary>
    /// Generate a JWT token for the given user and role
    /// </summary>
    /// <param name="user">The user to generate the token for</param>
    /// <param name="role">The role of the user</param>
    /// <returns>
    /// A tuple containing:
    /// <para>Item1 (string): The access token</para>
    /// <para>Item2 (int): The expiration time of the token in minutes</para>
    /// </returns>
    public (string accessToken, int expired) GenerateJwtToken(ApplicationUser user, string role);

    /// <summary>
    /// Get the email of the user from the given token
    /// </summary>
    /// <param name="token">The token to get the email from</param>
    /// <returns>The email of the user</returns>
    public string GetEmailPrincipal(string token);

    /// <summary>
    /// Generate a refresh token for the given token
    /// </summary>
    /// <param name="token">The token to generate the refresh token for</param>
    /// <returns>The refresh token</returns>
    public string GenerateRefreshToken(string token);

    /// <summary>
    /// Validate the refresh token
    /// </summary>
    /// <param name="token">The token to validate</param>
    /// <param name="refreshToken">The refresh token to validate</param>
    public bool ValidateRefreshToken(string token, string refreshToken);

    /// <summary>
    /// Get the principal from the expired token
    /// </summary>
    /// <param name="token">The token to get the principal from</param>
    /// <returns>The principal from the token</returns>
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
