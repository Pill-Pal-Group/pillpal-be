using PillPal.Application.Common.Interfaces.Payment;
using PillPal.Application.Features.Payments;

namespace PillPal.Infrastructure.PaymentService.ZaloPay;

public class ZaloPayConfiguration
{
    public string? AppId { get; set; }
    public string? AppUser { get; set; }
    public string? Key1 { get; set; }
    public string? Key2 { get; set; }
    public string? PaymentUrl { get; set; }
    public string? ReturnUrl { get; set; }
    public string? IpnUrl { get; set; }
}

public class ZaloPayService : IZaloPayService
{
    private readonly ZaloPayConfiguration Configuration;
    public ZaloPayService(IOptions<ZaloPayConfiguration> configuration)
    {
        Configuration = configuration.Value;
    }

    public (string paymentUrl, string zpTransToken) GetPaymentUrl(PaymentRequest request)
    {
        var zaloPayRequest = new ZaloPayRequest(
            appId: Configuration.AppId!,
            appUser: Configuration.AppUser!,
            appTime: (long)(DateTime.Now.ToUniversalTime() - DateTime.UnixEpoch).TotalMilliseconds,
            appTransId: DateTime.Now.ToString("yyMMdd") + "_" + Guid.NewGuid().ToString(),
            bankCode: "zalopayapp",

            amount: (long)request.Amount!,
            description: request.Description!
        );

        zaloPayRequest.MakeSignature(Configuration.Key1!);

        (string? paymentUrl, string? zpTransToken) = zaloPayRequest.GetLink(Configuration.PaymentUrl!);

        return (paymentUrl, zpTransToken);
    }
}
