namespace PillPal.Infrastructure.PaymentService.ZaloPay;

public class ZaloPayResponse
{
    public int returnCode { get; set; }
    public string? returnMessage { get; set; }
    public string? zpTransToken { get; set; }
    public string? orderUrl { get; set; }
}
