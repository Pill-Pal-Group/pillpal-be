namespace PillPal.Application.Features.DosageForms;

public record UpdateDosageFormDto
{
    /// <example>Tablet</example>
    public string? FormName { get; init; }
}

public class UpdateDosageFormValidator : AbstractValidator<UpdateDosageFormDto>
{
    public UpdateDosageFormValidator()
    {
        RuleFor(x => x.FormName)
            .NotEmpty()
            .MaximumLength(100);
    }
}
