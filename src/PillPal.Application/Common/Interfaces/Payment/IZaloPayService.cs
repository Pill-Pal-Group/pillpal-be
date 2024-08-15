using PillPal.Application.Features.Payments;

namespace PillPal.Application.Common.Interfaces.Payment;

public interface IZaloPayService
{
    /// <summary>
    /// Get payment url of ZaloPay
    /// </summary>
    /// <param name="request"></param>
    /// <returns>
    /// The task result contains a tuple of payment url and zpTransToken.
    /// </returns>
    (string paymentUrl, string zpTransToken) GetPaymentUrl(PaymentRequest request);
}
