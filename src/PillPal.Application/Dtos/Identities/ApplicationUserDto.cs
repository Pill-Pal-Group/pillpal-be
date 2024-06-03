namespace PillPal.Application.Dtos.Identities;

public record ApplicationUserDto
{
    public Guid Id { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
}
