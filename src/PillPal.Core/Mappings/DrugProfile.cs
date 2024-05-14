using PillPal.Core.Dtos.Drugs.Commands;
using PillPal.Core.Dtos.Drugs.Queries;

namespace PillPal.Core.Mappings;

public partial class MapperConfigure : Profile
{
    void DrugProfile()
    {
        CreateMap<Drug, CreateDrugCommand>()
            // .ForMember(dest => dest.Ingredients,
            //         opt => opt.MapFrom(src => src.Ingredients.Select(x => x.Id).ToList()))
            .ReverseMap();
        CreateMap<Drug, GetDrugQuery>().ReverseMap();
    }
}
