namespace PillPal.Core.Models;

public class ActiveIngredient : BaseAuditableEntity, IBaseEntity
{
    public Guid Id { get; set; }
    public string? IngredientCode { get; set; }
    public string? IngredientName { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = default!;
}
