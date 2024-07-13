namespace PillPal.Application.Features.ActiveIngredients;

public record ActiveIngredientQueryParameter : PaginationQueryParameter
{
    /// <example>API6060-555555</example>
    public string? IngredientCode { get; init; }

    /// <example>Paracetamol</example>
    public string? IngredientName { get; set; }
}

public static class ActiveIngredientQueryExtensions
{
    public static IQueryable<ActiveIngredient> Filter(this IQueryable<ActiveIngredient> query, ActiveIngredientQueryParameter queryParameter)
    {
        if (queryParameter.IngredientCode is not null)
        {
            query = query.Where(b => b.IngredientCode!.Contains(queryParameter.IngredientCode));
        }

        if (queryParameter.IngredientName is not null)
        {
            query = query.Where(b => b.IngredientName!.Contains(queryParameter.IngredientName));
        }

        return query;
    }
}
