namespace PillPal.Core.Models;

public class ScheduleDetail : BaseAuditableEntity
{
    public string? Time { get; set; }
    public string? DrugName { get; set; }
    public string? Quantity { get; set; }
    public string? Dose { get; set; }
    public string? Note { get; set; }

    public Guid ScheduleId { get; set; }
    public Schedule? Schedule { get; set; }
}
