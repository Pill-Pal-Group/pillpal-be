using PillPal.Application.Features.TermsOfServices;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void TermsOfServiceProfile()
    {
        CreateMap<TermsOfService, TermsOfServiceDto>().ReverseMap();
        CreateMap<TermsOfService, CreateTermsOfServiceDto>().ReverseMap();
        CreateMap<TermsOfService, UpdateTermsOfServiceDto>().ReverseMap();
    }
}
