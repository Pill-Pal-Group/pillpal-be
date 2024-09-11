using PillPal.Application.Features.PrescriptDetails;

namespace PillPal.Application.Features.Prescripts;

public record CreatePrescriptDto
{
    /// <example>https://monke.com/prescript-image.jpg</example>
    public string? PrescriptImage { get; init; }

    /// <example>2024-06-19</example>
    public DateTimeOffset ReceptionDate { get; init; }

    /// <example>Dr. Doof</example>
    public string? DoctorName { get; init; }

    /// <example>General Hospital</example>
    public string? HospitalName { get; init; }

    public IEnumerable<CreatePrescriptDetailDto> PrescriptDetails { get; init; } = default!;
}

public class CreatePrescriptValidator : AbstractValidator<CreatePrescriptDto>
{
    private const string PropertyRequired = "{PropertyName} is required.";
    public CreatePrescriptValidator()
    {
        RuleFor(p => p.PrescriptImage)
            .NotEmpty().WithMessage(PropertyRequired)
            .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

        RuleFor(p => p.ReceptionDate)
            .NotEmpty().WithMessage(PropertyRequired)
            .LessThan(DateTimeOffset.Now).WithMessage("{PropertyName} must be before {ComparisonValue}.");

        RuleFor(p => p.DoctorName)
            .NotEmpty().WithMessage(PropertyRequired)
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.HospitalName)
            .NotEmpty().WithMessage(PropertyRequired)
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.PrescriptDetails)
            .NotEmpty().WithMessage(PropertyRequired)
            .Must(p => p.Any()).WithMessage("{PropertyName} must have at least one item.")
            .ForEach(p => p.SetValidator(new CreatePrescriptDetailValidator()));
    }
}
