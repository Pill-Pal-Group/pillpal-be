namespace PillPal.Application.Dtos.DosageForms;

public record CreateDosageFormDto
{
    public string? FormName { get; init; }
}

public class CreateDosageFormValidator : AbstractValidator<CreateDosageFormDto>
{
    public CreateDosageFormValidator()
    {
        RuleFor(x => x.FormName)
            .NotEmpty()
            .MaximumLength(100);
    }
}
