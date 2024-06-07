namespace PillPal.Application.Features.DosageForms;

public record DosageFormDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>Tablet</example>
    public string? FormName { get; init; }
}
