namespace PillPal.Application.Features.Nations;

public record UpdateNationDto
{
    /// <example>Vietnam</example>
    public string? NationName { get; init; }
}

public class UpdateNationValidator : AbstractValidator<UpdateNationDto>
{
    public UpdateNationValidator()
    {
        RuleFor(x => x.NationName)
            .NotEmpty()
            .MaximumLength(100);
    }
}
