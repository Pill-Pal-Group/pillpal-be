namespace PillPal.Application.Features.Auths;

public class TokenLoginRequest
{
    public string? Token { get; init; }
}

public class TokenLoginRequestValidator : AbstractValidator<TokenLoginRequest>
{
    public TokenLoginRequestValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}
