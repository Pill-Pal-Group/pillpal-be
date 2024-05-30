using PillPal.Application.Dtos.Auths;

namespace PillPal.Application.Common.Interfaces.Auth;

public interface IAuthService
{
    public Task RegisterAsync(RegisterRequest request);
    public Task<AccessTokenResponse> TokenLoginAsync(TokenLoginRequest request);
    public Task<AccessTokenResponse> LoginAsync(LoginRequest request);
    public Task<AccessTokenResponse> RefreshTokenAsync(string refreshToken);
}
