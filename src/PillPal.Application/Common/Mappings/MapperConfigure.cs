namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    public MapperConfigure()
    {
        ActiveIngredientProfile();
        BrandProfile();
        CategoryProfile();
        CustomerPackageProfile();
        CustomerProfile();
        DosageFormProfile();
        MedicineProfile();
        NationProfile();
        PaymentProfile();
        PharmaceuticalCompanyProfile();
        PharmacyStoreProfile();
        PrescriptProfile();
        SpecificationProfile();
    }
}
