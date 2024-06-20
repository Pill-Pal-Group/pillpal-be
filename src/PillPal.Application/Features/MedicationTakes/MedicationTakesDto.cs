namespace PillPal.Application.Features.MedicationTakes;

public record MedicationTakesDto
{
    public DateTimeOffset DateTake { get; init; }
    public string? TimeTake { get; init; }
    public string? Dose { get; init; }
}
