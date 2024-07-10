using PillPal.Application.Common.Paginations;

namespace PillPal.Application.Features.PharmaceuticalCompanies;

public record PharmaceuticalCompanyQueryParameter : PaginationQueryParameter
{
    /// <example>PHC6060-555555</example>
    public string? CompanyCode { get; init; }

    /// <example>Doof</example>
    public string? CompanyName { get; set; }

    /// <example>USA</example>
    public string? Nation { get; set; }
}

public static class PharmaceuticalCompanyQueryExtensions
{
    public static IQueryable<PharmaceuticalCompany> Filter(this IQueryable<PharmaceuticalCompany> query, PharmaceuticalCompanyQueryParameter queryParameter)
    {
        if (queryParameter.CompanyCode is not null)
        {
            query = query.Where(c => c.CompanyCode!.Contains(queryParameter.CompanyCode));
        }

        if (queryParameter.CompanyName is not null)
        {
            query = query.Where(c => c.CompanyName!.Contains(queryParameter.CompanyName));
        }

        if (queryParameter.Nation is not null)
        {
            query = query.Where(c => c.Nation!.NationName!.Contains(queryParameter.Nation));
        }

        return query;
    }
}
