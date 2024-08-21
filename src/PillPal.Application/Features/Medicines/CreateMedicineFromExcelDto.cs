namespace PillPal.Application.Features.Medicines;

public record CreateMedicineFromExcelDto
{
    public string? MedicineName { get; init; }
    public bool RequirePrescript { get; init; }
    public string? Image { get; init; }
    public string? Specifications { get; init; }
    public string? RegistrationNumber { get; set; }

    // Ignore entities in mapper
    public string? DosageForms { get; init; }
    public List<string>? Categories { get; init; }
    public List<string>? ActiveIngredients { get; init; }
    public string? PharmaceuticalCompanies { get; init; }

    public string? Brand { get; init; }
    public string? Price { get; init; }
    public string? MedicineUrl { get; init; }
    public string? Nation { get; init; }
    public string? BrandUrl { get; init; }
    public string? BrandLogo { get; init; }
}
