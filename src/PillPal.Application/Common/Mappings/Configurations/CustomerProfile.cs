using PillPal.Application.Dtos.Customers;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Customer, CreateCustomerDto>().ReverseMap();
        CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
    }
}