namespace PillPal.Core.Models;

public class Specification : BaseAuditableEntity
{
    public string? TypeName { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = default!;
}
