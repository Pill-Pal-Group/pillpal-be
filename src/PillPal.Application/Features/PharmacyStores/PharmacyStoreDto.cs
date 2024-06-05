using PillPal.Application.Features.Brands;

namespace PillPal.Application.Features.PharmacyStores;

public record PharmacyStoreDto
{
    public Guid Id { get; init; }
    public string? StoreLocation { get; init; }
    public string? StoreImage { get; init; }
    public BrandDto Brand { get; init; } = default!;
}
