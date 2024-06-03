namespace PillPal.Core.Models;

public class Category : BaseAuditableEntity
{
    public string? CategoryCode { get; set; }
    public string? CategoryName { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = default!;
}
