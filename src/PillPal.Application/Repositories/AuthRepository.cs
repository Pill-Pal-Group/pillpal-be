using PillPal.Application.Common.Interfaces.Auth;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Dtos.Auths;

namespace PillPal.Application.Repositories;

public class AuthRepository(
    IServiceProvider serviceProvider,
    IIdentityService identityService,
    IJwtService jwtService)
    : BaseRepository(serviceProvider), IAuthService
{
    public async Task<AccessTokenResponse> LoginAsync(LoginRequest request)
    {
        var validator = GetValidator<LoginRequest>();

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = await identityService.LoginAsync(request.Email!, request.Password!);

        var token = jwtService.GenerateJwtToken(user.Item1, user.role);

        return new AccessTokenResponse
        {
            AccessToken = token,
            TokenType = "Bearer"
        };
    }

    public async Task<AccessTokenResponse> RefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        var validator = GetValidator<RegisterRequest>();

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await identityService.CreateUserAsync(request.Email!, request.Password!);
    }

    public async Task<AccessTokenResponse> TokenLoginAsync(TokenLoginRequest request)
    {
        var validator = GetValidator<TokenLoginRequest>();

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // jwtService.ValidateFirebaseToken(request.Token!);
        var email = jwtService.GetUserEmailFromToken(request.Token!);

        var user = await identityService.GetUserByEmailAsync(email);

        var token = jwtService.GenerateJwtToken(user.Item1, user.role);

        return new AccessTokenResponse
        {
            AccessToken = token,
            TokenType = "Bearer"
        };
    }
}
