namespace PillPal.Application.Dtos.PharmacyStores;

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
            .Empty()
            .MaximumLength(255);

        RuleFor(x => x.StoreImage)
            .Empty()
            .MaximumLength(255);

        RuleFor(x => x.BrandId)
            .NotEmpty();
    }
}
