namespace PillPal.Application.Features.Nations;

public record UpdateNationDto
{
    /// <example>VN</example>
    public string? NationCode { get; init; }

    /// <example>Vietnam</example>
    public string? NationName { get; init; }
}

public class UpdateNationValidator : AbstractValidator<UpdateNationDto>
{
    public UpdateNationValidator()
    {
        RuleFor(x => x.NationCode)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.NationName)
            .NotEmpty()
            .MaximumLength(100);
    }
}
