namespace PillPal.Core.Models;

public class ActiveIngredient : BaseAuditableEntity
{
    public string? IngredientCode { get; set; }
    public string? IngredientName { get; set; }
    public string? IngredientInformation { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = default!;
}
