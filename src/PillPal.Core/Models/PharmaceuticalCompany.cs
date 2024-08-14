namespace PillPal.Core.Models;

public class PharmaceuticalCompany : BaseAuditableEntity, IBaseEntity
{
    public Guid Id { get; set; }
    public string? CompanyCode { get; set; }
    public string? CompanyName { get; set; }

    public Guid NationId { get; set; }
    public virtual Nation? Nation { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = default!;
}
