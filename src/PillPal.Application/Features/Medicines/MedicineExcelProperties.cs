namespace PillPal.Application.Features.Medicines;

public record MedicineExcelProperties
{
    public string MedicineName { get; set; } = "Product Name";
    public string Image { get; set; } = "Image-src";
    public string MedicineUrl { get; set; } = "Link";
    public string Price { get; set; } = "Price";
    public string Categories { get; set; } = "Category";
    public string DosageForms { get; set; } = "Dosage forms";
    public string Specifications { get; set; } = "Specifications";
    public string ActiveIngredients { get; set; } = "Ingredient";
    public string PharmaceuticalCompanies { get; set; } = "Pharmaceuticals";
    public string Nation { get; set; } = "Nation";
    public string RegistrationNumber { get; set; } = "Registration number";
    public string RequirePrescript { get; set; } = "Medication requires prescription";
    public string BrandUrl { get; set; } = "Brand Url";
    public string Brand { get; set; } = "Brand";
    public string BrandLogo { get; set; } = "Brand Logo";
}

public record ExcelPropertyDelimiters
{
    public string IngredientDelimeter { get; set; } = ",";
    public string CategoryDelimeter { get; set; } = "/";
}
