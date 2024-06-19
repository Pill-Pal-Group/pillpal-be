using PillPal.Application.Features.PrescriptDetails;
using PillPal.Application.Features.Prescripts;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void PrescriptProfile()
    {
        CreateMap<Prescript, PrescriptDto>().ReverseMap();
        
        CreateMap<PrescriptDetail, PrescriptDetailDto>().ReverseMap();

        CreateMap<Prescript, CreatePrescriptDto>().ReverseMap();

        CreateMap<PrescriptDetail, CreatePrescriptDetailDto>().ReverseMap();
    }
}