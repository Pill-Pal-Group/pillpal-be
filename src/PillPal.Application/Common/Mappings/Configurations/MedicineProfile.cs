using PillPal.Application.Dtos.Brands;
using PillPal.Application.Dtos.Medicines;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void MedicineProfile()
    {
        CreateMap<Medicine, MedicineDto>().ReverseMap();
        CreateMap<Medicine, CreateMedicineDto>()
            .ReverseMap()
            .ForMember(dest => dest.SpecificationId, opt => opt.MapFrom(src => src.SpecificationId))
            .ForMember(
                dest => dest.Brands,
                opt => opt.MapFrom(
                    src => src.BrandIds.Select(pc => new BrandDto { Id = pc })));

        CreateMap<Medicine, UpdateMedicineDto>().ReverseMap();
    }
}