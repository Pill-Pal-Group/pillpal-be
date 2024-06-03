using PillPal.Application.Dtos.Brands;

namespace PillPal.Application.Dtos.PharmacyStores;

public record PharmacyStoreDto
{
    public Guid Id { get; init; }
    public string? StoreLocation { get; init; }
    public string? StoreImage { get; init; }
    public BrandDto Brand { get; init; } = default!;
}
