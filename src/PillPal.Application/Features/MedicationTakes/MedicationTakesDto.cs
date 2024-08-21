namespace PillPal.Application.Features.MedicationTakes;

public record MedicationTakesDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>2024-06-19</example>
    public DateTimeOffset DateTake { get; init; }

    /// <example>08:00</example>
    public string? TimeTake { get; init; }

    /// <example>2</example>
    public double Dose { get; init; }
}

public record MedicationTakesListDto
{
    /// <example>Paracetamol</example>
    public string? MedicineName { get; init; }

    /// <example>https://monke.com/med-image.jpg</example>
    public string? MedicineImage { get; init; }

    public IEnumerable<MedicationTakesDto> MedicationTakes { get; init; } = default!;
}
