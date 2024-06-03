namespace PillPal.Core.Models;

public class PackageCategory : BaseAuditableEntity
{
    public string? PackageTime { get; set; }
    public decimal Price { get; set; }

    public virtual ICollection<CustomerPackage> CustomerPackages { get; set; } = default!;
}
