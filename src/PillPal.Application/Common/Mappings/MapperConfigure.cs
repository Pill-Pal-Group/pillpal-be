namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    public MapperConfigure()
    {
        AccountProfile();
        ActiveIngredientProfile();
        BrandProfile();
        CategoryProfile();
        CustomerPackageProfile();
        CustomerProfile();
        DosageFormProfile();
        MedicineProfile();
        MedicationTakeProfile();
        NationProfile();
        PaymentProfile();
        PackageCategoryProfile();
        PharmaceuticalCompanyProfile();
        PharmacyStoreProfile();
        PrescriptProfile();
        SpecificationProfile();
        TermsOfServiceProfile();
    }
}
