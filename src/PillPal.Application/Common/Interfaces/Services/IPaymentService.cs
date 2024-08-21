using PillPal.Application.Features.Payments;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IPaymentService
{
    /// <summary>
    /// Create payment request
    /// </summary>
    /// <param name="packagePaymentInfo"></param>
    /// <returns>The task result contains the <see cref="PaymentResponse"/> representing the payment response.</returns>
    /// <exception cref="BadRequestException">Thrown if the payment method is invalid or unsupported.</exception>
    Task<PaymentResponse> CreatePaymentRequestAsync(CustomerPackagePaymentInformation packagePaymentInfo);
}
