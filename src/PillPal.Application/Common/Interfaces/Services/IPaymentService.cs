using PillPal.Application.Features.Payments;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IPaymentService
{
    Task<IEnumerable<PaymentDto>> GetPaymentsAsync();
    Task<PaymentDto> GetPaymentAsync(Guid paymentId);
    Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto);
    Task<IEnumerable<PaymentDto>> CreateBulkPaymentsAsync(IEnumerable<CreatePaymentDto> createPaymentDtos);
    Task<PaymentDto> UpdatePaymentAsync(Guid paymentId, UpdatePaymentDto updatePaymentDto);
}
