using PillPal.Application.Features.Nations;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void NationProfile()
    {
        CreateMap<Nation, NationDto>().ReverseMap();
        CreateMap<Nation, CreateNationDto>().ReverseMap();
        CreateMap<Nation, UpdateNationDto>().ReverseMap();
    }
}