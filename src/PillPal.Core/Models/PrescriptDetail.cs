namespace PillPal.Core.Models;

public class PrescriptDetail : BaseEntity, ISoftDelete
{
    public string? MedicineName { get; set; }
    public string? MedicineImage { get; set; }
    public DateTimeOffset DateStart { get; set; }
    public DateTimeOffset DateEnd { get; set; }
    public int TotalDose { get; set; }
    public double MorningDose { get; set; }
    public double NoonDose { get; set; }
    public double AfternoonDose { get; set; }
    public double NightDose { get; set; }
    public string? DosageInstruction { get; set; }

    public Guid PrescriptId { get; set; }
    public virtual Prescript? Prescript { get; set; }

    public virtual ICollection<MedicationTake> MedicationTakes { get; set; } = default!;
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
