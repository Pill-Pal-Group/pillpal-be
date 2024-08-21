namespace PillPal.Core.Models;

public class TermsOfService : BaseAuditableEntity, IBaseEntity
{
    public Guid Id { get; set; }
    public string? TosCode { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
}