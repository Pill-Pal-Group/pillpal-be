namespace PillPal.Core.Models;

public class Medicine : BaseAuditableEntity
{
    public string? MedicineCode { get; set; }
    public string? MedicineName { get; set; }
    public bool RequirePrescript { get; set; }
    public string? Image { get; set; }

    public Guid SpecificationId { get; set; }
    public virtual Specification? Specification { get; set; }

    public virtual ICollection<PharmaceuticalCompany> PharmaceuticalCompanies { get; set; } = default!;

    public virtual ICollection<Category> Categories { get; set; } = default!;

    public virtual ICollection<DosageForm> DosageForms { get; set; } = default!;

    public virtual ICollection<ActiveIngredient> ActiveIngredients { get; set; } = default!;

    public virtual ICollection<MedicineInBrand> MedicineInBrands { get; set; } = default!;
}
