namespace PillPal.Core.Models;

public class TermsOfService : BaseAuditableEntity
{
    public string? TosCode { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
}