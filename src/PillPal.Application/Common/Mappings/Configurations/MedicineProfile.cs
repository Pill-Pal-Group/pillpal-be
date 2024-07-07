using PillPal.Application.Features.MedicineInBrands;
using PillPal.Application.Features.Medicines;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void MedicineProfile()
    {
        CreateMap<Medicine, MedicineDto>().ReverseMap();

        CreateMap<Medicine, CreateMedicineDto>()
            .ReverseMap()
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            .ForMember(dest => dest.PharmaceuticalCompanies, opt => opt.Ignore())
            .ForMember(dest => dest.DosageForms, opt => opt.Ignore())
            .ForMember(dest => dest.ActiveIngredients, opt => opt.Ignore());

        CreateMap<Medicine, CreateMedicineFromExcelDto>()
            .ReverseMap()
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            .ForMember(dest => dest.PharmaceuticalCompanies, opt => opt.Ignore())
            .ForMember(dest => dest.DosageForms, opt => opt.Ignore())
            .ForMember(dest => dest.ActiveIngredients, opt => opt.Ignore());

        CreateMap<Medicine, UpdateMedicineDto>()
            .ReverseMap()
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            .ForMember(dest => dest.PharmaceuticalCompanies, opt => opt.Ignore())
            .ForMember(dest => dest.DosageForms, opt => opt.Ignore())
            .ForMember(dest => dest.ActiveIngredients, opt => opt.Ignore());

        CreateMap<MedicineInBrand, MedicineInBrandsDto>().ReverseMap();

        CreateMap<MedicineInBrand, CreateMedicineInBrandsDto>().ReverseMap();

        CreateMap<MedicineInBrand, UpdateMedicineInBrandsDto>().ReverseMap();
    }
}