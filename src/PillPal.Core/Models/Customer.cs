namespace PillPal.Core.Models;

public class Customer : BaseAuditableEntity, IBaseEntity
{
    public Guid Id { get; set; }
    public string? CustomerCode { get; set; }
    public DateTimeOffset? Dob { get; set; }
    public string? Address { get; set; }
    public TimeOnly BreakfastTime { get; set; }
    public TimeOnly LunchTime { get; set; }
    public TimeOnly AfternoonTime { get; set; }
    public TimeOnly DinnerTime { get; set; }
    public TimeSpan MealTimeOffset { get; set; }
    public int LockoutCount { get; set; }
    public string? DeviceToken { get; set; }

    public virtual ICollection<Prescript> Prescripts { get; set; } = default!;

    public virtual ICollection<CustomerPackage> CustomerPackages { get; set; } = default!;

    public Guid IdentityUserId { get; set; }
    public virtual ApplicationUser? IdentityUser { get; set; }
}
