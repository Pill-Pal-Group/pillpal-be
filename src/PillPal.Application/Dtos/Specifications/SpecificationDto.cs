namespace PillPal.Application.Dtos.Specifications;

public record SpecificationDto
{
    public Guid Id { get; init; }
    public string? TypeName { get; init; }
    public string? Detail { get; init; }
}
