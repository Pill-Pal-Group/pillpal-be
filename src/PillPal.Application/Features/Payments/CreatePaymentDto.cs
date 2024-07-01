namespace PillPal.Application.Features.Payments;

public record CreatePaymentDto
{
    /// <example>VnPay</example>
    public string? PaymentName { get; set; }

    /// <example>https://vnpay.vn/logo.png</example>
    public string? PaymentLogo { get; set; }
}

public class CreatePaymentValidator : AbstractValidator<CreatePaymentDto>
{
    public CreatePaymentValidator()
    {
        RuleFor(v => v.PaymentName)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.PaymentLogo)
            .MaximumLength(500)
            .NotEmpty();
    }
}
