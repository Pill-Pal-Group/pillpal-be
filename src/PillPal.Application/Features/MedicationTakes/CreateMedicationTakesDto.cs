namespace PillPal.Application.Features.MedicationTakes;

public record CreateMedicationTakesDto
{
    /// <example>2024-06-19</example>
    public DateTimeOffset DateTake { get; init; }

    /// <example>08:00</example>
    public string? TimeTake { get; init; }

    /// <example>2</example>
    public double Dose { get; init; }

    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid PrescriptDetailId { get; init; }
}

public class CreateMedicationTakesValidator : AbstractValidator<CreateMedicationTakesDto>
{
    private const string PropertyRequired = "{PropertyName} is required.";
    public CreateMedicationTakesValidator()
    {
        RuleFor(x => x.DateTake)
            .NotEmpty().WithMessage(PropertyRequired);

        RuleFor(x => x.TimeTake)
            .NotEmpty().WithMessage(PropertyRequired)
            .Matches(@"^(?:[01]\d|2[0123]):(?:[012345]\d)$")
            .WithMessage("{PropertyName} must be in HH:mm format.");

        RuleFor(x => x.Dose)
            .NotEmpty().WithMessage(PropertyRequired)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(x => x.PrescriptDetailId)
            .NotEmpty().WithMessage(PropertyRequired);
    }
}
