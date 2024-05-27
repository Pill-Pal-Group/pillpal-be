namespace PillPal.Application.Dtos.DosageForms;

public record DosageFormDto
{
    public Guid Id { get; init; }
    public string? FormName { get; init; }
}
