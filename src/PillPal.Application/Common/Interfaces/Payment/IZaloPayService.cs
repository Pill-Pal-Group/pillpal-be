using PillPal.Application.Features.Payments;

namespace PillPal.Application.Common.Interfaces.Payment;

public interface IZaloPayService
{
    string GetPaymentUrl(PaymentRequest request);
}
