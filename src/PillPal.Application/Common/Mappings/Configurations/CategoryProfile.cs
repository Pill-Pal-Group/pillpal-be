using PillPal.Application.Features.Categories;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void CategoryProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
    }
}