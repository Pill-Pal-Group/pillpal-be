namespace PillPal.Application.Features.Payments;

public record UpdatePaymentDto
{
    /// <example>VnPay</example>
    public string? PaymentName { get; set; }

    /// <example>https://vnpay.vn/logo.png</example>
    public string? PaymentLogo { get; set; }
}

public class UpdatePaymentValidator : AbstractValidator<UpdatePaymentDto>
{
    public UpdatePaymentValidator()
    {
        RuleFor(v => v.PaymentName)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.PaymentLogo)
            .MaximumLength(500)
            .NotEmpty();
    }
}
