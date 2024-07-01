using PillPal.Application.Features.Payments;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void PaymentProfile()
    {
        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<Payment, CreatePaymentDto>().ReverseMap();
        CreateMap<Payment, UpdatePaymentDto>().ReverseMap();
    }
}