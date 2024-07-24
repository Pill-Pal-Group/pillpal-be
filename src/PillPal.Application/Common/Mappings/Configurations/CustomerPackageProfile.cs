using PillPal.Application.Features.CustomerPackages;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void CustomerPackageProfile()
    {
        CreateMap<CustomerPackage, CustomerPackageDto>().ReverseMap();
    }
}