namespace PillPal.Core.Models;

public class Category : BaseAuditableEntity, IBaseEntity
{
    public Guid Id { get; set; }
    public string? CategoryCode { get; set; }
    public string? CategoryName { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = default!;
}
