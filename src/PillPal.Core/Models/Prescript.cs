namespace PillPal.Core.Models;

public class Prescript : BaseAuditableEntity
{
    public string? PrescriptCode { get; set; }
    public string? PrescriptName { get; set; }
    public string? PrescriptDescription { get; set; }
    public string? DoctorName { get; set; }
    public string? HospitalName { get; set; }
    public DateTimeOffset DateIssued { get; set; }

    public ICollection<DrugPrescript> DrugPrescripts { get; set; } = default!;

    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
}
