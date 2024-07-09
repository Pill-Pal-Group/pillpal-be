namespace PillPal.Application.Features.PrescriptDetails;

public record PrescriptDetailDto
{
    /// <example>Paracetamol</example>
    public string? MedicineName { get; init; }

    /// <example>https://monke.com/med-image.jpg</example>
    public string? MedicineImage { get; init; }

    /// <example>2024-06-19</example>
    public DateTimeOffset DateStart { get; init; }

    /// <example>2024-06-29</example>
    public DateTimeOffset DateEnd { get; init; }

    /// <example>80</example>
    public int TotalDose { get; init; }

    /// <example>2</example>
    public double MorningDose { get; set; }

    /// <example>2</example>
    public double NoonDose { get; set; }

    /// <example>2</example>
    public double AfternoonDose { get; set; }

    /// <example>2</example>
    public double NightDose { get; set; }

    /// <example>Aftermeal</example>
    public string? DosageInstruction { get; init; }
}
