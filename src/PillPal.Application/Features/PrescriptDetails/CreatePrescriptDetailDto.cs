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
    private const string PropertyRequired = "{PropertyName} is required.";
    private const string PropertyEqualOrGreaterThanZero = "{PropertyName} must be greater than or equal to 0.";
    public CreatePrescriptDetailValidator()
    {
        RuleFor(p => p.MedicineName)
            .NotEmpty().WithMessage(PropertyRequired)
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.MedicineImage)
            .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

        RuleFor(p => p.DateStart)
            .NotEmpty().WithMessage(PropertyRequired)
            .LessThan(p => p.DateEnd).WithMessage("{PropertyName} must be before {ComparisonValue}.");

        RuleFor(p => p.DateEnd)
            .NotEmpty().WithMessage(PropertyRequired)
            .GreaterThan(p => p.DateStart).WithMessage("{PropertyName} must be after {ComparisonValue}.");

        RuleFor(p => p.TotalDose)
            .NotEmpty().WithMessage(PropertyRequired)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.MorningDose)
            .GreaterThanOrEqualTo(0).WithMessage(PropertyEqualOrGreaterThanZero);

        RuleFor(p => p.NoonDose)
            .GreaterThanOrEqualTo(0).WithMessage(PropertyEqualOrGreaterThanZero);

        RuleFor(p => p.AfternoonDose)
            .GreaterThanOrEqualTo(0).WithMessage(PropertyEqualOrGreaterThanZero);

        RuleFor(p => p.NightDose)
            .GreaterThanOrEqualTo(0).WithMessage(PropertyEqualOrGreaterThanZero);

        RuleFor(p => p.DosageInstruction)
            .NotEmpty().WithMessage(PropertyRequired)
            .IsEnumName(typeof(DosageInstructionEnums))
            .WithMessage("{PropertyName} is not valid. Valid values are: Aftermeal, Beforemeal.");
    }
}
