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
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Content)
            .NotEmpty();
    }
}