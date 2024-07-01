namespace PillPal.Application.Features.Nations;

public record NationQueryParameter
{
    /// <example>VNR</example>
    public string? NationCode { get; init; }
}

public static class NationQueryExtensions
{
    public static IQueryable<Nation> Filter(this IQueryable<Nation> query, NationQueryParameter queryParameter)
    {
        if (queryParameter.NationCode is not null)
        {
            query = query.Where(n => n.NationCode!.Contains(queryParameter.NationCode));
        }

        return query;
    }
}
