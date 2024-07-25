namespace PillPal.Infrastructure.PaymentService.VnPay;

public class VnPayConfiguration
{
    public string? TmnCode { get; set; }
    public string? HashSecret { get; set; }
    public string? PaymentUrl { get; set; }
    public string? ReturnUrl { get; set; }
    public string? Version { get; set; }
}
