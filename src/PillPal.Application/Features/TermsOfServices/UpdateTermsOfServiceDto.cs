namespace PillPal.Application.Features.TermsOfServices;

public record UpdateTermsOfServiceDto
{
    /// <example>Security Policy</example>
    public string? Title { get; init; }

    /// <example>Content of the security policy.</example>
    public string? Content { get; init; }
}

public class UpdateTermsOfServiceValidator : AbstractValidator<UpdateTermsOfServiceDto>
{
    public UpdateTermsOfServiceValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(500).WithMessage("Title must not exceed 500 characters.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.");
    }
}