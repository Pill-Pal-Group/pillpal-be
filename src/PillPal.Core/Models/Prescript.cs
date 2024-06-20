namespace PillPal.Core.Models;

public class Prescript : BaseAuditableEntity
{
    public string? PrescriptImage { get; set; }
    public DateTimeOffset ReceptionDate { get; set; }
    public string? DoctorName { get; set; }
    public string? HospitalName { get; set; }

    public Guid CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }

    public virtual ICollection<PrescriptDetail> PrescriptDetails { get; set; } = default!;
}
