namespace PillPal.Core.Models;

public class Drug : BaseAuditableEntity
{
    public string? DrugCode { get; set; }
    public string? DrugName { get; set; }
    public string? DrugDescription { get; set; }
    public string? SideEffect { get; set; }
    public string? Indication { get; set; }
    public string? Contraindication { get; set; }
    public string? Warning { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<Ingredient> Ingredients { get; set; } = default!;

    public ICollection<DrugInformation> DrugInformations { get; set; } = default!;

    public ICollection<DrugPrescript> DrugPrescripts { get; set; } = default!;

    public ICollection<PharmacyStore> PharmacyStores { get; set; } = default!;
}
