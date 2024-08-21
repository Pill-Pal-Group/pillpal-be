namespace PillPal.Infrastructure.PaymentService.ZaloPay;

public class ZaloPayResponse
{
    public int ReturnCode { get; set; }
    public string? ReturnMessage { get; set; }
    public string? ZpTransToken { get; set; }
    public string? OrderUrl { get; set; }
}
