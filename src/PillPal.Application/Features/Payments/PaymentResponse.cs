namespace PillPal.Application.Features.Payments;

public record PaymentResponse
{
    /// <example>https://sandbox.monkepay.com/paylink</example>
    public string? PaymentUrl { get; set; }

    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid CustomerPackageId { get; init; }

    /// <example>zp-monkepaywithdoofTransToken</example>
    public string? ZpTransToken { get; init; }
}
