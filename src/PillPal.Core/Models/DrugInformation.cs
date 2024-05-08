namespace PillPal.Core.Models;

public class DrugInformation : BaseAuditableEntity
{
    public string? Manufacturer { get; set; }
    public string? RegisterCompany { get; set; }
    public string? RegisterNumber { get; set; }
    public string? RegisterDate { get; set; }
    public string? RegisterPlace { get; set; }
    public string? Composition { get; set; }
    public string? StorageCondition { get; set; }
    public string? Instruction { get; set; }
    public string? Overdose { get; set; }
    public string? Interaction { get; set; }
    public string? Precaution { get; set; }
    public string? Price { get; set; }
    public string? Status { get; set; }
    public string? Note { get; set; }

    public Guid DrugId { get; set; }
    public Drug? Drug { get; set; }
}
