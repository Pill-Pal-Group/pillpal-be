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
    public CreateMedicationTakesValidator()
    {
        RuleFor(x => x.DateTake)
            .NotEmpty();

        RuleFor(x => x.TimeTake)
            .NotEmpty()
            .Matches(@"^(?:[01]\d|2[0123]):(?:[012345]\d)$")
            .WithMessage("{PropertyName} must be in HH:mm format.");

        RuleFor(x => x.Dose)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.PrescriptDetailId)
            .NotEmpty();
    }
}
