namespace PillPal.Core.Models;

public class Nation : BaseAuditableEntity
{
    public string? NationName { get; set; }

    public virtual ICollection<PharmaceuticalCompany> PharmaceuticalCompanies { get; set; } = default!;
}
