using PillPal.Application.Features.ActiveIngredients;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void ActiveIngredientProfile()
    {
        CreateMap<ActiveIngredient, ActiveIngredientDto>().ReverseMap();
        CreateMap<ActiveIngredient, CreateActiveIngredientDto>().ReverseMap();
        CreateMap<ActiveIngredient, UpdateActiveIngredientDto>().ReverseMap();
    }
}