namespace PillPal.Application.Features.Nations;

public record NationDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>VNR</example>
    public string? NationCode { get; init; }

    /// <example>Vietnam</example>
    public string? NationName { get; init; }
}