namespace PillPal.Core.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public Customer? Customer { get; set; }
}
