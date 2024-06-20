namespace PillPal.Application.Features.PrescriptDetails;

public record CreatePrescriptDetailDto
{
    /// <example>Paracetamol</example>
    public string? MedicineName { get; init; }

    /// <example>2024-06-19</example>
    public DateTimeOffset DateStart { get; init; }

    /// <example>2024-06-29</example>
    public DateTimeOffset DateEnd { get; init; }

    /// <example>10</example>
    public int Total { get; init; }
}

public class CreatePrescriptDetailValidator : AbstractValidator<CreatePrescriptDetailDto>
{
    public CreatePrescriptDetailValidator()
    {
        RuleFor(p => p.MedicineName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.DateStart)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .LessThan(p => p.DateEnd).WithMessage("{PropertyName} must be before {ComparisonValue}.");

        RuleFor(p => p.DateEnd)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(p => p.DateStart).WithMessage("{PropertyName} must be after {ComparisonValue}.");

        RuleFor(p => p.Total)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
    }
}
