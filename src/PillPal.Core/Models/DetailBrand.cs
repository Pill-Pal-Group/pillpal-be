namespace PillPal.Core.Models;

public class DetailBrand : BaseAuditableEntity
{
    public string? BrandName { get; set; }
    public string? BrandImageUrl { get; set; }
    public string? BrandWebsite { get; set; }

    public ICollection<PharmacyStore> PharmacyStores { get; set; } = default!;
}
