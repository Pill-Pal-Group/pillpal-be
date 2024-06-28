namespace PillPal.Application.Features.Auths;

public record ChangePasswordRequest
{
    /// <example>P@ssword7</example>
    public string? CurrentPassword { get; init; }

    /// <example>P@ssword8</example>
    public string? NewPassword { get; init; }

    /// <example>P@ssword8</example>
    public string? ConfirmPassword { get; init; }
}

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Current password is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non alphanumeric character.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.NewPassword)
            .WithMessage("Confirm password must match new password");
    }
}
