namespace PillPal.Core.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public Customer? Customer { get; set; }
    public Pharmacist? Pharmacist { get; set; }
}
