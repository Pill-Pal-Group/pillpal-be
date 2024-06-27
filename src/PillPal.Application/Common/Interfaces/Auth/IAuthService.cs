using PillPal.Application.Features.Auths;

namespace PillPal.Application.Common.Interfaces.Auth;

public interface IAuthService
{
    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="request">The request to register</param>
    /// <exception cref="ValidationException">Thrown when the request is invalid</exception>
    /// <exception cref="ConflictException">Thrown when the user is already registered</exception>
    public Task RegisterAsync(RegisterRequest request);

    /// <summary>
    /// Login with pregiven token
    /// </summary>
    /// <param name="request">The request to login</param>
    /// <returns>The response containing the access token</returns>
    /// <exception cref="ValidationException">Thrown when the request is invalid</exception>
    public Task<AccessTokenResponse> TokenLoginAsync(TokenLoginRequest request);

    /// <summary>
    /// Login with the given email and password
    /// </summary>
    /// <param name="request">The request to login</param>
    /// <returns>The response containing the access token</returns>
    /// <exception cref="ValidationException">Thrown when the request is invalid</exception>
    public Task<AccessTokenResponse> LoginAsync(LoginRequest request);

    /// <summary>
    /// Refresh the access token
    /// </summary>
    /// <param name="refreshToken">The request to refresh the token</param>
    /// <returns>The response containing the access token</returns>
    /// <exception cref="ValidationException">Thrown when the request is invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when the token is invalid</exception>
    public Task<AccessTokenResponse> RefreshTokenAsync(RefreshRequest refreshToken);
}
