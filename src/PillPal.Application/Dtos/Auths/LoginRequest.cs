namespace PillPal.Application.Dtos.Auths;

public record LoginRequest
{
    public string? Email { get; init; }
    public string? Password { get; init; }
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
