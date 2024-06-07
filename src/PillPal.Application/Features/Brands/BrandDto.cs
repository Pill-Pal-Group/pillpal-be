namespace PillPal.Application.Features.Brands;

public record BrandDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>BRD6060-555555</example>
    public string? BrandCode { get; init; }

    /// <example>https://monke.com/brand</example>
    public string? BrandUrl { get; init; }

    /// <example>https://monke.com/brandlogo.jpg</example>
    public string? BrandLogo { get; init; }
}
