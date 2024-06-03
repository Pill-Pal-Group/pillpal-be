namespace PillPal.Core.Models;

public class PharmacyStore : BaseAuditableEntity
{
    public string? StoreLocation { get; set; }
    public string? StoreImage { get; set; }

    public Guid BrandId { get; set; }
    public virtual Brand? Brand { get; set; }
}
