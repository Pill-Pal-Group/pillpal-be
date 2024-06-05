using PillPal.Application.Dtos.Medicines;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void MedicineProfile()
    {
        CreateMap<Medicine, MedicineDto>().ReverseMap();

        CreateMap<Medicine, CreateMedicineDto>()
            .ReverseMap()
            .ForMember(dest => dest.PharmaceuticalCompanies, opt => opt.Ignore())
            .ForMember(dest => dest.DosageForms, opt => opt.Ignore())
            .ForMember(dest => dest.ActiveIngredients, opt => opt.Ignore())
            .ForMember(dest => dest.Brands, opt => opt.Ignore());

        CreateMap<Medicine, UpdateMedicineDto>()
            .ReverseMap()
            .ForMember(dest => dest.PharmaceuticalCompanies, opt => opt.Ignore())
            .ForMember(dest => dest.DosageForms, opt => opt.Ignore())
            .ForMember(dest => dest.ActiveIngredients, opt => opt.Ignore())
            .ForMember(dest => dest.Brands, opt => opt.Ignore());
    }
}