using PillPal.Application.Features.Brands;

namespace PillPal.Application.Features.MedicineInBrands;

public record MedicineInBrandsDto
{
    public BrandDto? Brand { get; init; }

    /// <example>8.000₫/viên</example>
    public string? Price { get; init; }

    /// <example>https://monke.com/paracetamol</example>
    public string? MedicineUrl { get; init; }
}
