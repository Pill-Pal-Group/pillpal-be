namespace PillPal.Application.Features.Nations;

public record NationDto
{
    public Guid Id { get; init; }
    public string? NationCode { get; init; }
    public string? NationName { get; init; }
}