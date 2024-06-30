using PillPal.Application.Features.PackageCategories;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void PackageCategoryProfile()
    {
        CreateMap<PackageCategory, PackageCategoryDto>().ReverseMap();
        CreateMap<PackageCategory, CreatePackageCategoryDto>().ReverseMap();
        CreateMap<PackageCategory, UpdatePackageCategoryDto>().ReverseMap();
    }
}
