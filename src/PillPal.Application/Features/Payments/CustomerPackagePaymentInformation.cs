namespace PillPal.Application.Features.Payments;

public record CustomerPackagePaymentInformation
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid PackageCategoryId { get; init; }

    public PaymentEnums PaymentType { get; init; }
}

public class CustomerPackagePaymentInformationValidator : AbstractValidator<CustomerPackagePaymentInformation>
{
    public CustomerPackagePaymentInformationValidator()
    {
        RuleFor(v => v.PackageCategoryId)
            .NotEmpty().WithMessage("PackageId is required.");

        RuleFor(v => v.PaymentType)
            .IsInEnum().WithMessage("PaymentType is not valid or not supported.");
    }
}
