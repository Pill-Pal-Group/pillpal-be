namespace PillPal.Application.Features.Nations;

public record CreateNationDto
{
    public string? NationCode { get; init; }
    public string? NationName { get; init; }
}

public class CreateNationValidator : AbstractValidator<CreateNationDto>
{
    public CreateNationValidator()
    {
        RuleFor(x => x.NationCode)
            .NotEmpty()
            .MaximumLength(10);

        RuleFor(x => x.NationName)
            .NotEmpty()
            .MaximumLength(50);
    }
}
