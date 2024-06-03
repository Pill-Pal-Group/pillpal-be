using PillPal.Application.Dtos.PharmacyStores;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void PharmacyStoreProfile()
    {
        CreateMap<PharmacyStore, PharmacyStoreDto>().ReverseMap();
        CreateMap<PharmacyStore, CreatePharmacyStoreDto>().ReverseMap();
        CreateMap<PharmacyStore, UpdatePharmacyStoreDto>().ReverseMap();
    }
}