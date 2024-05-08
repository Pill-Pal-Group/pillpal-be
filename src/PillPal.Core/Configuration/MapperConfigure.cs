namespace PillPal.Core.Mappings;

public partial class MapperConfigure : Profile
{
    public MapperConfigure()
    {
        DrugProfile();
        PrescriptProfile();
        PharmacistProfile();
        CustomerProfile();
        DrugInformationProfile();
    }
}
