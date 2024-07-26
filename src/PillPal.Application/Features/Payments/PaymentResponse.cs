namespace PillPal.Application.Features.Payments;

public record PaymentResponse
{
    public string? PaymentUrl { get; set; }
    public Guid CustomerPackageId { get; init; }
}
