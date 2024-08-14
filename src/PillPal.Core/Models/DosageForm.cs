namespace PillPal.Core.Models;

public class DosageForm : BaseAuditableEntity, IBaseEntity
{
    public Guid Id { get; set; }
    public string? FormName { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = default!;
}
