namespace PillPal.Core.Models;

public class PrescriptDetail : BaseAuditableEntity
{
    public string? MedicineName { get; set; }
    public DateTimeOffset DateStart { get; set; }
    public DateTimeOffset DateEnd { get; set; }
    public int Total { get; set; }

    public Guid PrescriptId { get; set; }
    public virtual Prescript? Prescript { get; set; }

    public virtual ICollection<MedicationTake> MedicationTakes { get; set; } = default!;
}
