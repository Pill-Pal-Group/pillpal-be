namespace PillPal.Application.Features.TermsOfServices;

public record TermsOfServiceQueryParameter
{
    /// <example>TOS6060-555555</example>
    public string? TosCode { get; init; }

    /// <example>Security Policy</example>
    public string? Title { get; init; }
}

public static class TermsOfServiceQueryExtensions
{
    public static IQueryable<TermsOfService> Filter(this IQueryable<TermsOfService> query, TermsOfServiceQueryParameter queryParameter)
    {
        if (queryParameter.TosCode is not null)
        {
            query = query.Where(t => t.TosCode!.Contains(queryParameter.TosCode));
        }

        if (queryParameter.Title is not null)
        {
            query = query.Where(t => t.Title!.Contains(queryParameter.Title));
        }

        return query;
    }
}
