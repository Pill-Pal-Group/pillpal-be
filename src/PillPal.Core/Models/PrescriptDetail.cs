namespace PillPal.Core.Models;

public class PrescriptDetail : BaseAuditableEntity
{
    public string? MedicineName { get; set; }
    public DateTimeOffset DateStart { get; set; }
    public DateTimeOffset DateEnd { get; set; }
    public int Total { get; set; }

    public Guid MedicineId { get; set; }
    public virtual Medicine? Medicine { get; set; }

    public virtual ICollection<Prescript> Prescripts { get; set; } = default!;
}
