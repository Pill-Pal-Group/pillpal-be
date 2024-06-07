namespace PillPal.Application.Features.Specifications;

public record SpecificationDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>Box</example>
    public string? TypeName { get; init; }

    /// <example>2 per tablet</example>
    public string? Detail { get; init; }
}
