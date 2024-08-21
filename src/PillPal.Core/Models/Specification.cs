namespace PillPal.Core.Models;

public class Specification : BaseAuditableEntity, IBaseEntity
{
    public Guid Id { get; set; }
    public string? TypeName { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = default!;
}
