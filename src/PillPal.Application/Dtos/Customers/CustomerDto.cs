using PillPal.Application.Dtos.Identities;

namespace PillPal.Application.Dtos.Customers;

public record CustomerDto
{
    public Guid Id { get; init; }
    public string? CustomerCode { get; init; }
    public DateTimeOffset? Dob { get; init; }
    public string? Address { get; init; }
    public ApplicationUserDto ApplicationUser { get; init; } = default!;
}
