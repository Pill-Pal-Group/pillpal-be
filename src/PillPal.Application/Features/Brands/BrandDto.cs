namespace PillPal.Application.Features.Brands;

public record BrandDto
{
    public Guid Id { get; init; }
    public string? BrandCode { get; init; }
    public string? BrandUrl { get; init; }
    public string? BrandLogo { get; init; }
}
