namespace PillPal.Core.Dtos.Ingredients.Queries;

public record GetIngredientQuery(
    Guid Id,
    string IngredientCode,
    string IngredientName,
    string IngredientDescription,
    string IngredientType,
    string ImageUrl
);
