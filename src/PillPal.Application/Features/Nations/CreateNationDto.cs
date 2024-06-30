namespace PillPal.Application.Features.Nations;

public record CreateNationDto
{
    /// <example>VNR</example>
    public string? NationCode { get; init; }

    /// <example>Vietnam</example>
    public string? NationName { get; init; }
}

public class CreateNationValidator : AbstractValidator<CreateNationDto>
{
    public CreateNationValidator()
    {
        RuleFor(x => x.NationCode)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.NationName)
            .NotEmpty()
            .MaximumLength(100);
    }
}
