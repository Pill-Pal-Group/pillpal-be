namespace PillPal.Application.Features.MedicineInBrands;

public record MedicinePriceUnitsDto
{
    public int TotalCount { get; init; }
    public IEnumerable<string> PriceUnits { get; init; } = default!;
}
