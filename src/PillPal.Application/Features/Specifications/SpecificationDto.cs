namespace PillPal.Application.Features.Specifications;

public record SpecificationDto
{
    public Guid Id { get; init; }
    public string? TypeName { get; init; }
    public string? Detail { get; init; }
}
