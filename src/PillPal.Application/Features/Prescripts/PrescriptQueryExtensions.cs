namespace PillPal.Application.Features.Prescripts;

public record PrescriptQueryParameter
{
    /// <example>CUS6060-555555</example>
    public string? CustomerCode { get; init; }
}

public record PrescriptIncludeParameter
{
    /// <example>true</example>
    public bool IncludePrescriptDetails { get; init; }
}

public static class PrescriptQueryExtensions
{
    public static IQueryable<Prescript> Filter(this IQueryable<Prescript> query, PrescriptQueryParameter queryParameter)
    {
        if (queryParameter.CustomerCode is not null)
        {
            query = query
                .Where(p => p.Customer!.CustomerCode!.Contains(queryParameter.CustomerCode));
        }

        return query;
    }

    public static IQueryable<Prescript> Include(this IQueryable<Prescript> query, PrescriptIncludeParameter includeParameter)
    {
        if (includeParameter.IncludePrescriptDetails)
        {
            query = query.Include(p => p.PrescriptDetails);
        }

        return query;
    }
}
