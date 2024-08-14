namespace PillPal.Core.Models;

public class Nation : BaseAuditableEntity, IBaseEntity
{
    public Guid Id { get; set; }
    public string? NationName { get; set; }

    public virtual ICollection<PharmaceuticalCompany> PharmaceuticalCompanies { get; set; } = default!;
}
