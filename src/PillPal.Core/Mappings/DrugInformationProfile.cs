using PillPal.Core.Dtos.DrugInformations.Commands;
using PillPal.Core.Dtos.DrugInformations.Queries;

namespace PillPal.Core.Mappings;

public partial class MapperConfigure : Profile
{
    void DrugInformationProfile()
    {
        CreateMap<DrugInformation, GetDrugInformationQuery>().ReverseMap();
        CreateMap<DrugInformation, CreateDrugInformationCommand>().ReverseMap();
    }
}