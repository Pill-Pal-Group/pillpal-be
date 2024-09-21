namespace PillPal.Application.Features.TermsOfServices;

public record CreateTermsOfServiceDto
{
    /// <example>Security Policy</example>
    public string? Title { get; init; }

    /// <example>Content of the security policy.</example>
    public string? Content { get; init; }
}

public class CreateTermsOfServiceValidator : AbstractValidator<CreateTermsOfServiceDto>
{
    public CreateTermsOfServiceValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Content)
            .NotEmpty();
    }
}
