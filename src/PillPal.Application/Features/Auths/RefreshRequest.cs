namespace PillPal.Application.Features.Auths;

public record RefreshRequest
{
    public string? ExpiredToken { get; init; }
    public string? RefreshToken { get; init; }
}

public class RefreshRequestValidator : AbstractValidator<RefreshRequest>
{
    public RefreshRequestValidator()
    {
        RuleFor(x => x.ExpiredToken).NotEmpty();
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}
