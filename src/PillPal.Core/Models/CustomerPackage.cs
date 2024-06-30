namespace PillPal.Core.Models;

public class CustomerPackage : BaseEntity
{
    public int Duration { get; set; }
    public int RemainDay { get; set; }
    public decimal Price { get; set; }

    public Guid CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }

    public Guid PackageId { get; set; }
    public virtual PackageCategory? PackageCategory { get; set; }

    public Guid PaymentId { get; set; }
    public virtual Payment? Payment { get; set; }
}
