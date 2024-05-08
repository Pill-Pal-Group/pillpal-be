using Microsoft.AspNetCore.Identity;

namespace PillPal.Core.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public Customer? Customer { get; set; }
    public Pharmacist? Pharmacist { get; set; }
}
