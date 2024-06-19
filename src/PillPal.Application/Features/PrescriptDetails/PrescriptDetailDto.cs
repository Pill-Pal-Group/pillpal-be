namespace PillPal.Application.Features.PrescriptDetails;

public record PrescriptDetailDto
{
    /// <example>Paracetamol</example>
    public string? MedicineName { get; init; }

    /// <example>2024-06-19</example>
    public DateTimeOffset DateStart { get; init; }

    /// <example>2024-06-29</example>
    public DateTimeOffset DateEnd { get; init; }

    /// <example>10</example>
    public int Total { get; init; }
}
