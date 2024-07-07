namespace PillPal.Core.Models;

public class MedicineInBrand : ISoftDelete
{
    public Guid BrandId { get; set; }
    public virtual Brand? Brand { get; set; }

    public Guid MedicineId { get; set; }
    public virtual Medicine? Medicine { get; set; }

    public string? Price { get; set; }
    public string? MedicineUrl { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
