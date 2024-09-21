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
    public CreatePrescriptValidator()
    {
        RuleFor(p => p.PrescriptImage)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(p => p.ReceptionDate)
            .NotEmpty()
            .LessThan(DateTimeOffset.Now);

        RuleFor(p => p.DoctorName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(p => p.HospitalName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(p => p.PrescriptDetails)
            .NotEmpty()
            .Must(p => p.Any()).WithMessage("{PropertyName} must have at least one item.")
            .ForEach(p => p.SetValidator(new CreatePrescriptDetailValidator()));
    }
}
