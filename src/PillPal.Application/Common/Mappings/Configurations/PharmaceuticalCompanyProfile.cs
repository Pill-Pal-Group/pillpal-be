using PillPal.Application.Features.PharmaceuticalCompanies;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void PharmaceuticalCompanyProfile()
    {
        CreateMap<PharmaceuticalCompany, PharmaceuticalCompanyDto>().ReverseMap();
        CreateMap<PharmaceuticalCompany, CreatePharmaceuticalCompanyDto>().ReverseMap();
        CreateMap<PharmaceuticalCompany, UpdatePharmaceuticalCompanyDto>().ReverseMap();
    }
}