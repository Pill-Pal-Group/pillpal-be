using PillPal.Application.Common.Interfaces.Payment;
using PillPal.Application.Features.Payments;

namespace PillPal.Infrastructure.PaymentService.VnPay;

public class VnPayService : IVnPayService
{
    private readonly VnPayConfiguration _configuration;
    private readonly IUser _user;
    public VnPayService(IOptions<VnPayConfiguration> configuration, IUser user)
    {
        _configuration = configuration.Value;
        _user = user;
    }

    public string GetPaymentUrl(PaymentRequest request)
    {
        var vnpRequest = new VnPayRequest(
            version: _configuration.Version!,
            tmnCode: _configuration.TmnCode!,
            returnUrl: _configuration.ReturnUrl!,
            ipAddress: _user.IpAddress!,
            txnRef: request.PaymentReference!,
            amount: request.Amount ?? 0,
            orderInfo: request.Description!
        );

        return vnpRequest.GetLink(_configuration.PaymentUrl!, _configuration.HashSecret!);
    }
}
