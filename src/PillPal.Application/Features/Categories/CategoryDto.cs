namespace PillPal.Application.Features.Categories;

public record CategoryDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>CAT6060-555555</example>
    public string? CategoryCode { get; init; }

    /// <example>Vaccines</example>
    public string? CategoryName { get; init; }
}
