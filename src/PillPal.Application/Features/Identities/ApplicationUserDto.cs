namespace PillPal.Application.Features.Identities;

public record ApplicationUserDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>Monke</example>
    public string? UserName { get; init; }

    /// <example>monke@mail.com</example>
    public string? Email { get; init; }

    /// <example>094278290</example>
    public string? PhoneNumber { get; init; }
}
