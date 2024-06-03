namespace PillPal.Application.Dtos.DosageForms;

public record UpdateDosageFormDto
{
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
