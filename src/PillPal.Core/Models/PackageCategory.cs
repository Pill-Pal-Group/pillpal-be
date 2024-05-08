namespace PillPal.Core.Models;

public class PackageCategory : BaseEntity
{
    public string? PackageCategoryName { get; set; }

    public ICollection<CustomerPackage> CustomerPackages { get; set; } = default!;
}
