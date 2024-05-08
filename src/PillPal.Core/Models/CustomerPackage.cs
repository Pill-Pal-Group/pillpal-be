namespace PillPal.Core.Models;

public class CustomerPackage : BaseAuditableEntity
{
    public string? PackageName { get; set; }
    public string? PackageType { get; set; }
    public decimal Price { get; set; }
    public int Months { get; set; }

    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public Guid PackageCategoryId { get; set; }
    public PackageCategory? PackageCategory { get; set; }
}
