namespace PillPal.Application.Features.PharmacyStores;

public record UpdatePharmacyStoreDto
{
    public string? StoreLocation { get; init; }
    public string? StoreImage { get; init; }
    public Guid BrandId { get; init; }
}

public class UpdatePharmacyStoreValidator : AbstractValidator<UpdatePharmacyStoreDto>
{
    public UpdatePharmacyStoreValidator()
    {
        RuleFor(x => x.StoreLocation)
            .MaximumLength(500);

        RuleFor(x => x.StoreImage)
            .MaximumLength(500);

        RuleFor(x => x.BrandId)
            .NotEmpty();
    }
}
