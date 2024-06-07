namespace PillPal.Application.Features.PharmacyStores;

public record CreatePharmacyStoreDto
{
    /// <example>Q9, HCM, VN</example>
    public string? StoreLocation { get; init; }

    /// <example>https://monke.com/store.jpg</example>
    public string? StoreImage { get; init; }

    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid BrandId { get; init; }
}

public class CreatePharmacyStoreValidator : AbstractValidator<CreatePharmacyStoreDto>
{
    public CreatePharmacyStoreValidator()
    {
        RuleFor(x => x.StoreLocation)
            .MaximumLength(500);

        RuleFor(x => x.StoreImage)
            .MaximumLength(500);

        RuleFor(x => x.BrandId)
            .NotEmpty();
    }
}
