namespace PillPal.Core.Models;

public class CustomerPackage : IBaseEntity
{
    public Guid Id { get; set; }
    public int Duration { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public decimal Price { get; set; }
    public bool IsExpired { get; set; }
    public int PaymentStatus { get; set; }

    public string? PaymentReference { get; set; }

    public Guid CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }

    public Guid PackageCategoryId { get; set; }
    public virtual PackageCategory? PackageCategory { get; set; }
}
