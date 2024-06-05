namespace PillPal.Application.Features.DosageForms;

public record DosageFormDto
{
    public Guid Id { get; init; }
    public string? FormName { get; init; }
}
