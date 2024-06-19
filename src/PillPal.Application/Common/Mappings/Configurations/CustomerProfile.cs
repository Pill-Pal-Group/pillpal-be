using PillPal.Application.Features.Customers;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(c => c.ApplicationUser, dest => dest.MapFrom(src => src.IdentityUser))
            .ReverseMap();

        CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
    }
}