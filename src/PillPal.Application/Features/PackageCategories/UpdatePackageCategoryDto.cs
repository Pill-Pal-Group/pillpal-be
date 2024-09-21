namespace PillPal.Application.Features.PackageCategories;

public record UpdatePackageCategoryDto
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

public class UpdatePackageCategoryValidator : AbstractValidator<UpdatePackageCategoryDto>
{
    public UpdatePackageCategoryValidator()
    {
        RuleFor(x => x.PackageName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.PackageDescription)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.PackageDuration)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0);
    }
}
