using PillPal.Application.Features.Payments;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IPaymentService
{
    Task<PaymentResponse> CreatePaymentRequestAsync(CustomerPackagePaymentInformation packagePaymentInfo);
    Task UpdatePaymentStatusAsync(string paymentRef, PaymentStatusEnums paymentStatus);
}
