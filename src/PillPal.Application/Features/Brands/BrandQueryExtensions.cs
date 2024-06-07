namespace PillPal.Application.Features.Brands;

public record BrandQueryParameter
{
    /// <example>BRD6060-555555</example>
    public string? BrandCode { get; init; }
}

public static class BrandQueryExtensions
{
    public static IQueryable<Brand> Filter(this IQueryable<Brand> query, BrandQueryParameter queryParameter)
    {
        if (queryParameter.BrandCode is not null)
        {
            query = query.Where(b => b.BrandCode!.Contains(queryParameter.BrandCode));
        }

        return query;
    }
}
