namespace PillPal.Core.Models;

public class DrugPrescript
{
    public string? Dosage { get; set; }
    public string? Schedule { get; set; }
    public string? Quantity { get; set; }

    public Guid DrugId { get; set; }
    public Drug? Drug { get; set; }

    public Guid PrescriptId { get; set; }
    public Prescript? Prescript { get; set; }
}
