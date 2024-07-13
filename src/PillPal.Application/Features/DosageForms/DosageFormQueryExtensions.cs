namespace PillPal.Application.Features.DosageForms;

public record DosageFormQueryParameter : PaginationQueryParameter
{
    /// <example>Tablet</example>
    public string? FormName { get; init; }
}

public static class DosageFormQueryExtensions
{
    public static IQueryable<DosageForm> Filter(this IQueryable<DosageForm> query, DosageFormQueryParameter queryParameter)
    {
        if (queryParameter.FormName is not null)
        {
            query = query.Where(d => d.FormName!.Contains(queryParameter.FormName));
        }

        return query;
    }
}
