using PillPal.Application.Dtos.Brands;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void BrandProfile()
    {
        CreateMap<Brand, BrandDto>().ReverseMap();
        CreateMap<Brand, CreateBrandDto>().ReverseMap();
        CreateMap<Brand, UpdateBrandDto>().ReverseMap();
    }
}