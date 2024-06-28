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
        IdentityProfile();
        MedicineProfile();
        MedicationTakeProfile();
        NationProfile();
        PaymentProfile();
        PharmaceuticalCompanyProfile();
        PharmacyStoreProfile();
        PrescriptProfile();
        SpecificationProfile();
        TermsOfServiceProfile();
    }
}
