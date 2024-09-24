namespace PillPal.Application.Features.Accounts;

public record AssignManagerRequest
{
    /// <example>manager@mail.com</example>
    public string? Email { get; set; }

    /// <example>P@assword7</example>
    public string? Password { get; set; }

    /// <example>0942782905</example>
    public string? PhoneNumber { get; set; }
}

public class AssignManagerRequestValidator : AbstractValidator<AssignManagerRequest>
{
    public AssignManagerRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non alphanumeric character.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d{10}$")
            .WithMessage("Phone number must be 10 digits.");
    }
}
