using PillPal.Application.Features.Brands;

namespace PillPal.Application.Features.MedicineInBrands;

public record MedicineInBrandsDto
{
    public BrandDto? Brand { get; init; }

    /// <example>8000</example>
    public decimal Price { get; init; }

    /// <example>VND</example>
    public string? PriceUnit { get; init; }

    /// <example>https://monke.com/paracetamol</example>
    public string? MedicineUrl { get; init; }

    /// <example>2024-07-31T00:00:00+00:00</example>
    public DateTimeOffset UpdatedAt { get; init; }
}
