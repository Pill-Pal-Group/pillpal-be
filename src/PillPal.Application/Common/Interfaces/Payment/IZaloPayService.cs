using PillPal.Application.Features.Payments;

namespace PillPal.Application.Common.Interfaces.Payment;

public interface IZaloPayService
{
    (string zpMsg, string zpTransToken) GetPaymentUrl(PaymentRequest request);
}
