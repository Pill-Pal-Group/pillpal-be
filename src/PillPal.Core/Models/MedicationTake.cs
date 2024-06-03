namespace PillPal.Core.Models;

public class MedicationTake : BaseAuditableEntity
{
    public DateTimeOffset DateTake { get; set; }
    public string? TimeTake { get; set; }
    public string? Dose { get; set; }

    public virtual ICollection<Prescript> Prescripts { get; set; } = default!;

    public Guid MedicineId { get; set; }
    public virtual Medicine? Medicine { get; set; }
}
