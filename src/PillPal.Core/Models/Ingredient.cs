namespace PillPal.Core.Models;

public class Ingredient : BaseAuditableEntity
{
    public string? IngredientCode { get; set; }
    public string? IngredientName { get; set; }
    public string? IngredientDescription { get; set; }
    public string? IngredientType { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<Drug> Drugs { get; set; } = default!;
}
