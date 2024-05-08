namespace PillPal.Core.Models;

public class Pharmacist : BaseEntity
{
    public string? PharmacistCode { get; set; }
    public string? PharmacistName { get; set; }
    public string? LicenseNumber { get; set; }

    public Guid IdentityUserId { get; set; }
    public ApplicationUser? IdentityUser { get; set; }
}
