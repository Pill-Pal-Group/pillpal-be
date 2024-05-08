namespace PillPal.Core.Models;

public class Customer : BaseEntity
{
    public string? CustomerCode { get; set; }
    public DateTimeOffset? Dob { get; set; }
    public string? Address { get; set; }

    public ICollection<CustomerPackage> CustomerPackages { get; set; } = default!;
    public ICollection<Prescript> Prescripts { get; set; } = default!;
    public ICollection<Schedule> Schedules { get; set; } = default!;

    public Guid IdentityUserId { get; set; }
    public ApplicationUser? IdentityUser { get; set; }
}
