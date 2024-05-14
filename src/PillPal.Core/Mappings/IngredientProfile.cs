using PillPal.Core.Dtos.Ingredients.Commands;
using PillPal.Core.Dtos.Ingredients.Queries;

namespace PillPal.Core.Mappings;

public partial class MapperConfigure : Profile
{
    void IngredientProfile()
    {
        CreateMap<Ingredient, GetIngredientQuery>().ReverseMap();
        CreateMap<Ingredient, GetIngredientQueryId>().ReverseMap();
        CreateMap<Ingredient, CreateIngredientCommand>().ReverseMap();
    }
}
