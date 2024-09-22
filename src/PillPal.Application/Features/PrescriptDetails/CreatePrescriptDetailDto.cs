namespace PillPal.Application.Features.PrescriptDetails;

public record CreatePrescriptDetailDto
{
    /// <example>Paracetamol</example>
    public string? MedicineName { get; init; }

    /// <example>https://monke.com/med-image.jpg</example>
    public string? MedicineImage { get; init; }

    /// <example>2024-06-19</example>
    public DateTimeOffset DateStart { get; init; }

    /// <example>2024-06-29</example>
    public DateTimeOffset DateEnd { get; init; }

    /// <example>80</example>
    public int TotalDose { get; init; }

    /// <example>2</example>
    public double MorningDose { get; set; }

    /// <example>2</example>
    public double NoonDose { get; set; }

    /// <example>2</example>
    public double AfternoonDose { get; set; }

    /// <example>2</example>
    public double NightDose { get; set; }

    /// <example>Aftermeal</example>
    public string? DosageInstruction { get; init; }
}

public class CreatePrescriptDetailValidator : AbstractValidator<CreatePrescriptDetailDto>
{
    public CreatePrescriptDetailValidator()
    {
        RuleFor(p => p.MedicineName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(p => p.MedicineImage)
            .MaximumLength(500);

        RuleFor(p => p.DateStart)
            .NotEmpty()
            .LessThan(p => p.DateEnd)
            .WithMessage("{PropertyName} must be less than DateEnd.");

        RuleFor(p => p.DateEnd)
            .NotEmpty()
            .GreaterThan(p => p.DateStart)
            .WithMessage("{PropertyName} must be greater than DateStart.");

        RuleFor(p => p.TotalDose)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.MorningDose)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.NoonDose)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.AfternoonDose)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.NightDose)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.DosageInstruction)
            .NotEmpty()
            .IsEnumName(typeof(DosageInstructionEnums))
            .WithMessage("{PropertyName} is not valid. Valid values are: Aftermeal, Beforemeal.");
    }
}
