namespace PillPal.Application.Features.ActiveIngredients;

public record ActiveIngredientQueryParameter
{
    public string? IngredientCode { get; init; }
}

public static class ActiveIngredientQueryExtensions
{
    public static IQueryable<ActiveIngredient> Filter(this IQueryable<ActiveIngredient> query, ActiveIngredientQueryParameter queryParameter)
    {
        if (queryParameter.IngredientCode is not null)
        {
            query = query.Where(b => b.IngredientCode!.Contains(queryParameter.IngredientCode));
        }

        return query;
    }
}
