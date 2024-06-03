namespace PillPal.Core.Models;

public class Specification : BaseEntity
{
    public string? TypeName { get; set; }
    public string? Detail { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = default!;
}
