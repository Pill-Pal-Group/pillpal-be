using PillPal.Application.Common.Interfaces.Auth;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;
using PillPal.Application.Features.Customers;
using System.Security.Claims;

namespace PillPal.Application.Features.Auths;

public class AuthRepository(
    ICustomerService customerService,
    IServiceProvider serviceProvider,
    IIdentityService identityService,
    IFirebaseService firebaseService,
    IJwtService jwtService)
    : BaseRepository(serviceProvider), IAuthService
{
    public async Task<AccessTokenResponse> LoginAsync(LoginRequest request)
    {
        await ValidateAsync(request);

        var user = await identityService.LoginAsync(request.Email!, request.Password!);

        var (accessToken, expired) = jwtService.GenerateJwtToken(user.Item1, user.role);

        var refreshToken = jwtService.GenerateRefreshToken(accessToken);

        return new AccessTokenResponse
        {
            AccessToken = accessToken,
            TokenType = "Bearer",
            RefreshToken = refreshToken,
            ExpiresIn = expired
        };
    }

    public async Task<AccessTokenResponse> RefreshTokenAsync(RefreshRequest refreshToken)
    {
        await ValidateAsync(refreshToken);

        var isTokenValid = jwtService.ValidateRefreshToken(refreshToken.ExpiredToken!, refreshToken.RefreshToken!);

        if (!isTokenValid)
        {
            throw new UnauthorizedAccessException("Invalid token");
        }

        var claimsPrincipal = jwtService.GetPrincipalFromExpiredToken(refreshToken.ExpiredToken!);

        var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;

        var user = await identityService.GetUserByEmailAsync(email!);

        var (accessToken, expired) = jwtService.GenerateJwtToken(user.Item1, user.role);

        var newRefreshToken = jwtService.GenerateRefreshToken(accessToken);

        return new AccessTokenResponse
        {
            AccessToken = accessToken,
            TokenType = "Bearer",
            RefreshToken = newRefreshToken,
            ExpiresIn = expired
        };
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        await ValidateAsync(request);

        var userId = await identityService.CreateUserAsync(request.Email!, request.Password!);

        CreateCustomerDto customer = new()
        {
            IdentityUserId = Guid.Parse(userId)
        };

        await customerService.CreateCustomerAsync(customer);
    }

    public async Task<AccessTokenResponse> TokenLoginAsync(TokenLoginRequest request)
    {
        await ValidateAsync(request);

        var email = await firebaseService.GetEmailFromTokenAsync(request.Token!);

        var user = await identityService.GetUserByEmailAsync(email);

        var (accessToken, expired) = jwtService.GenerateJwtToken(user.Item1, user.role);

        var refreshToken = jwtService.GenerateRefreshToken(accessToken);

        return new AccessTokenResponse
        {
            AccessToken = accessToken,
            TokenType = "Bearer",
            RefreshToken = refreshToken,
            ExpiresIn = expired
        };
    }
}
