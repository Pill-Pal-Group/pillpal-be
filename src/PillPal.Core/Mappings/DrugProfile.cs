using PillPal.Core.Dtos.Drug.Commands;
using PillPal.Core.Dtos.Drug.Queries;

namespace PillPal.Core.Mappings;

public partial class MapperConfigure : Profile
{
    void DrugProfile()
    {
        CreateMap<Drug, CreateDrugCommand>().ReverseMap();
        CreateMap<Drug, GetDrugQuery>().ReverseMap();
    }
}
