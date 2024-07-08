namespace PillPal.Application.Features.ActiveIngredients;

public record ActiveIngredientDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>API6060-555555</example>
    public string? IngredientCode { get; init; }

    /// <example>Paracetamol</example>
    public string? IngredientName { get; init; }
}
