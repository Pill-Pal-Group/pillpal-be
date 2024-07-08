namespace PillPal.Application.Features.Nations;

public record CreateNationDto
{
    /// <example>Vietnam</example>
    public string? NationName { get; init; }
}

public class CreateNationValidator : AbstractValidator<CreateNationDto>
{
    public CreateNationValidator()
    {
        RuleFor(x => x.NationName)
            .NotEmpty()
            .MaximumLength(100);
    }
}
