namespace PillPal.Application.Features.Nations;

public record NationQueryParameter
{
    public string? NationCode { get; init; }
    public string? NationName { get; init; }
}

public static class NationQueryExtensions
{
    public static IQueryable<Nation> Filter(this IQueryable<Nation> query, NationQueryParameter queryParameter)
    {
        if (queryParameter.NationCode is not null)
        {
            query = query.Where(n => n.NationCode!.Contains(queryParameter.NationCode));
        }

        if (queryParameter.NationName is not null)
        {
            query = query.Where(n => n.NationName!.Contains(queryParameter.NationName));
        }

        return query;
    }
}
