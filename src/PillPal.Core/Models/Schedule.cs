namespace PillPal.Core.Models;

public class Schedule : BaseAuditableEntity
{
    public string? ScheduleName { get; set; }
    public ICollection<ScheduleDetail> ScheduleDetails { get; set; } = default!;

    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
}

