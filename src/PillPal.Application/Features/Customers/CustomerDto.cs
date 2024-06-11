using PillPal.Application.Features.Identities;

namespace PillPal.Application.Features.Customers;

public record CustomerDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>CUS6060-555555</example>
    public string? CustomerCode { get; init; }

    /// <example>2002-01-01</example>
    public DateTimeOffset? Dob { get; init; }

    /// <example>Q9, HCMC, Vietnam</example>
    public string? Address { get; init; }
    public ApplicationUserDto IdentityUser { get; init; } = default!;
}
