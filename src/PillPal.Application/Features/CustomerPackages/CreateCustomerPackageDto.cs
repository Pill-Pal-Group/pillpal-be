namespace PillPal.Application.Features.CustomerPackages;

public record CreateCustomerPackageDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid PackageCategoryId { get; init; }

    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid PaymentId { get; init; }
}

public class CreateCustomerPackageValidator : AbstractValidator<CreateCustomerPackageDto>
{
    public CreateCustomerPackageValidator()
    {
        RuleFor(v => v.PackageCategoryId)
            .NotEmpty().WithMessage("PackageId is required.");

        RuleFor(v => v.PaymentId)
            .NotEmpty().WithMessage("PaymentId is required.");
    }
}
