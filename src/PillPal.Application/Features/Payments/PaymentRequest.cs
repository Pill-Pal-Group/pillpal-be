namespace PillPal.Application.Features.Payments;

public record PaymentRequest
{
    public decimal? Amount { get; set; }
    public string? Description { get; set; }
    public string? PaymentReference { get; set; }
}