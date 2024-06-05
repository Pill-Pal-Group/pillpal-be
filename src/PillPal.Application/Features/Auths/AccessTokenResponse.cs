namespace PillPal.Application.Features.Auths;

public record AccessTokenResponse
{
    public string? AccessToken { get; init; }
    public string? RefreshToken { get; init; }
    public string? TokenType { get; init; }
    public int ExpiresIn { get; init; }
}
