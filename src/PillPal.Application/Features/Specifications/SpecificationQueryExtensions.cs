using PillPal.Application.Common.Paginations;

namespace PillPal.Application.Features.Specifications;

public record SpecificationQueryParameter : PaginationQueryParameter
{
    /// <example>Box</example>
    public string? TypeName { get; set; }
}

public static class SpecificationQueryExtensions
{
    public static IQueryable<Specification> Filter(this IQueryable<Specification> query, SpecificationQueryParameter queryParameter)
    {
        if (queryParameter.TypeName is not null)
        {
            query = query.Where(s => s.TypeName!.Contains(queryParameter.TypeName));
        }

        return query;
    }
}
