using PillPal.Application.Features.Payments;

namespace PillPal.Application.Common.Interfaces.Payment;

public interface IVnPayService
{
    string GetPaymentUrl(PaymentRequest request);
}
