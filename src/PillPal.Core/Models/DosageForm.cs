namespace PillPal.Core.Models;

public class DosageForm : BaseEntity
{
    public string? FormName { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = default!;
}
