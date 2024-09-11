namespace PillPal.Application.Features.PackageCategories;

public record CreatePackageCategoryDto
{
    /// <example>Premium 1 month</example>
    public string? PackageName { get; init; }

    /// <example>Package for premium feature in 1 month</example>
    public string? PackageDescription { get; init; }

    /// <example>30</example>
    public int PackageDuration { get; init; }

    /// <example>100000</example>
    public decimal Price { get; init; }
}

public class CreatePackageCategoryValidator : AbstractValidator<CreatePackageCategoryDto>
{
    private const string PropertyRequired = "{PropertyName} is required.";
    public CreatePackageCategoryValidator()
    {
        RuleFor(x => x.PackageName)
            .NotEmpty().WithMessage(PropertyRequired)
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(x => x.PackageDescription)
            .NotEmpty().WithMessage(PropertyRequired)
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");

        RuleFor(x => x.PackageDuration)
            .NotEmpty().WithMessage(PropertyRequired)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage(PropertyRequired)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
    }
}
