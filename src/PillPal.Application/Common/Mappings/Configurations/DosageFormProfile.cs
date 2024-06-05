using PillPal.Application.Features.DosageForms;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void DosageFormProfile()
    {
        CreateMap<DosageForm, DosageFormDto>().ReverseMap();
        CreateMap<DosageForm, CreateDosageFormDto>().ReverseMap();
        CreateMap<DosageForm, UpdateDosageFormDto>().ReverseMap();
    }
}