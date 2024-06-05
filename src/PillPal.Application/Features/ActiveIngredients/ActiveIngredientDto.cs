namespace PillPal.Application.Features.ActiveIngredients;

public record ActiveIngredientDto
{
    public Guid Id { get; init; }
    public string? IngredientCode { get; init; }
    public string? IngredientName { get; init; }
    public string? IngredientInformation { get; init; }
}
