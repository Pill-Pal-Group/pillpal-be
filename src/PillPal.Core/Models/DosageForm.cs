namespace PillPal.Core.Models;

public class DosageForm : BaseAuditableEntity
{
    public string? FormName { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = default!;
}
