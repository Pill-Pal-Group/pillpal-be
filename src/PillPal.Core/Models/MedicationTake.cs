namespace PillPal.Core.Models;

public class MedicationTake : BaseEntity, ISoftDelete
{
    public DateTimeOffset DateTake { get; set; }
    public string? TimeTake { get; set; }
    public double Dose { get; set; }

    public Guid PrescriptDetailId { get; set; }
    public virtual PrescriptDetail? PrescriptDetail { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
