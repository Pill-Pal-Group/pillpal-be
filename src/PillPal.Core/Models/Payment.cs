namespace PillPal.Core.Models;

public class Payment : BaseEntity
{
    public string? PaymentName { get; set; }
    public string? PaymentLogo { get; set; }

    public virtual ICollection<CustomerPackage> CustomerPackages { get; set; } = default!;
}
