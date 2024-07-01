namespace PillPal.Application.Features.Payments;

public record PaymentDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; set; }

    /// <example>VnPay</example>
    public string? PaymentName { get; set; }

    /// <example>https://vnpay.vn/logo.png</example>
    public string? PaymentLogo { get; set; }
}
