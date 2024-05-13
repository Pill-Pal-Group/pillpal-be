namespace PillPal.Core.Common;

public class BaseAuditableEntity : BaseEntity, ISoftDelete
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    protected BaseAuditableEntity()
    {
        Id = Guid.NewGuid();
        //CreatedAt = DateTimeOffset.Now;
        //UpdatedAt = null;
        //CreatedBy = null;
        //UpdatedBy = null;
        //IsDeleted = false;
        //DeletedAt = null;
    }
}
