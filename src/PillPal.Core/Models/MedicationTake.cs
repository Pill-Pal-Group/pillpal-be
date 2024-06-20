namespace PillPal.Core.Models;

public class MedicationTake : BaseAuditableEntity
{
    public DateTimeOffset DateTake { get; set; }
    public string? TimeTake { get; set; }
    public string? Dose { get; set; }

    public Guid PrescriptDetailId { get; set; }
    public virtual PrescriptDetail? PrescriptDetail { get; set; }
}
