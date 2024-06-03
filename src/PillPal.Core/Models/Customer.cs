namespace PillPal.Core.Models;

public class Customer : BaseEntity
{
    public string? CustomerCode { get; set; }
    public DateTimeOffset? Dob { get; set; }
    public string? Address { get; set; }

    public virtual ICollection<Prescript> Prescripts { get; set; } = default!;

    public virtual ICollection<CustomerPackage> CustomerPackages { get; set; } = default!;

    public Guid IdentityUserId { get; set; }
    public virtual ApplicationUser? IdentityUser { get; set; }
}
