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
        PackageCategoryProfile();
        PharmaceuticalCompanyProfile();
        PrescriptProfile();
        SpecificationProfile();
        TermsOfServiceProfile();
    }
}
