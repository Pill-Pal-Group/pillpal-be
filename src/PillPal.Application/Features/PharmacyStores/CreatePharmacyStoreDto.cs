namespace PillPal.Application.Features.PharmacyStores;

public record CreatePharmacyStoreDto
{
    public string? StoreLocation { get; init; }
    public string? StoreImage { get; init; }
    public Guid BrandId { get; init; }
}

public class CreatePharmacyStoreValidator : AbstractValidator<CreatePharmacyStoreDto>
{
    public CreatePharmacyStoreValidator()
    {
        RuleFor(x => x.StoreLocation)
            .MaximumLength(255);

        RuleFor(x => x.StoreImage)
            .MaximumLength(255);

        RuleFor(x => x.BrandId)
            .NotEmpty();
    }
}
