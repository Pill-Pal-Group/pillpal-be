using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.Payments;

public class PaymentRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IPaymentService
{
    public async Task<IEnumerable<PaymentDto>> CreateBulkPaymentsAsync(IEnumerable<CreatePaymentDto> createPaymentDtos)
    {
        await ValidateListAsync(createPaymentDtos);

        var payments = Mapper.Map<IEnumerable<Payment>>(createPaymentDtos);

        await Context.Payments.AddRangeAsync(payments);

        await Context.SaveChangesAsync();

        return Mapper.Map<IEnumerable<PaymentDto>>(payments);
    }

    public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto)
    {
        await ValidateAsync(createPaymentDto);

        var payment = Mapper.Map<Payment>(createPaymentDto);

        await Context.Payments.AddAsync(payment);

        await Context.SaveChangesAsync();

        return Mapper.Map<PaymentDto>(payment);
    }

    public async Task<PaymentDto> GetPaymentAsync(Guid paymentId)
    {
        var payment = await Context.Payments
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == paymentId)
            ?? throw new NotFoundException(nameof(Payment), paymentId);

        return Mapper.Map<PaymentDto>(payment);
    }

    public async Task<IEnumerable<PaymentDto>> GetPaymentsAsync()
    {
        var payments = await Context.Payments
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<PaymentDto>>(payments);
    }

    public async Task<PaymentDto> UpdatePaymentAsync(Guid paymentId, UpdatePaymentDto updatePaymentDto)
    {
        await ValidateAsync(updatePaymentDto);

        var payment = await Context.Payments
            .FirstOrDefaultAsync(p => p.Id == paymentId)
            ?? throw new NotFoundException(nameof(Payment), paymentId);

        Mapper.Map(updatePaymentDto, payment);

        await Context.SaveChangesAsync();

        return Mapper.Map<PaymentDto>(payment);
    }
}
