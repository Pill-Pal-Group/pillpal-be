using PillPal.Application.Dtos.Specifications;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void SpecificationProfile()
    {
        CreateMap<Specification, SpecificationDto>().ReverseMap();
        CreateMap<Specification, CreateSpecificationDto>().ReverseMap();
        CreateMap<Specification, UpdateSpecificationDto>().ReverseMap();
    }
}