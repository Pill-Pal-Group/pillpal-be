using PillPal.Application.Features.Brands;

namespace PillPal.Application.Features.PharmacyStores;

public record PharmacyStoreDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>Q9, HCM, VN</example>
    public string? StoreLocation { get; init; }

    /// <example>https://monke.com/store.jpg</example>
    public string? StoreImage { get; init; }
    public BrandDto Brand { get; init; } = default!;
}
