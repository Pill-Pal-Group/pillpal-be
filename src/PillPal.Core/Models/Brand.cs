namespace PillPal.Core.Models;

public class Brand : BaseAuditableEntity
{
    public string? BrandCode { get; set; }
    public string? BrandName { get; set; }
    public string? BrandUrl { get; set; }
    public string? BrandLogo { get; set; }

    public virtual ICollection<MedicineInBrand> MedicineInBrands { get; set; } = default!;
}
