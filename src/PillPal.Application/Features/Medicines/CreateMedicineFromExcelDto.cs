namespace PillPal.Application.Features.Medicines;

public record CreateMedicineFromExcelDto
{
    public string? MedicineName { get; init; }
    public bool RequirePrescript { get; init; }
    public string? Image { get; init; }
    public string? Specifications { get; set; }

    // Ignore entities in mapper
    public string? Categories { get; set; }
    public string? DosageForms { get; set; }
    public string? ActiveIngredients { get; set; }
    public string? PharmaceuticalCompanies { get; set; }

    public string? Brand { get; set; }
    public string? Price { get; set; }
    public string? MedicineUrl { get; set; }
    public string? Nation { get; set; }
}
