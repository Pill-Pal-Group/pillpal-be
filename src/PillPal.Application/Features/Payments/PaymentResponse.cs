namespace PillPal.Application.Features.Payments;

public record PaymentResponse
{
    public string? PaymentUrl { get; set; }
    public Guid CustomerPackageId { get; init; }
    public string zp_trans_token { get; init; }
}
