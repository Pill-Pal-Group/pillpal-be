namespace PillPal.Core.Models;

public class PharmacyStore : BaseAuditableEntity
{
    public string? StoreName { get; set; }
    public string? StoreAddress { get; set; }
    public string? StorePhone { get; set; }
    public string? StoreEmail { get; set; }
    public string? StoreImage { get; set; }

    public ICollection<Drug> Drugs { get; set; } = default!;

    public Guid BrandId { get; set; }
    public DetailBrand? DetailBrand { get; set; }
}
