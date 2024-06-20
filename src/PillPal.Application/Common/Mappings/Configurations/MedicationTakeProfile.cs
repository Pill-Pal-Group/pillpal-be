using PillPal.Application.Features.MedicationTakes;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void MedicationTakeProfile()
    {
        CreateMap<MedicationTake, MedicationTakesDto>().ReverseMap();
    }
}
