namespace PillPal.Core.Models;

public class Prescript : BaseAuditableEntity, IBaseEntity
{
    public Guid Id { get; set; }
    public string? PrescriptImage { get; set; }
    public DateTimeOffset ReceptionDate { get; set; }
    public string? DoctorName { get; set; }
    public string? HospitalName { get; set; }

    public Guid CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }

    public virtual ICollection<PrescriptDetail> PrescriptDetails { get; set; } = default!;
}
