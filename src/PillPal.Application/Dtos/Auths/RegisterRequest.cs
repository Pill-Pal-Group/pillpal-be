namespace PillPal.Application.Dtos.Auths;

public record RegisterRequest
{
    public string? Email { get; init; }
    public string? Password { get; init; }
}

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
