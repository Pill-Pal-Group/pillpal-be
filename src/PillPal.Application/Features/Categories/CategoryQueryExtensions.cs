using PillPal.Application.Common.Paginations;

namespace PillPal.Application.Features.Categories;

public record CategoryQueryParameter : PaginationQueryParameter
{
    /// <example>CAT6060-555555</example>
    public string? CategoryCode { get; init; }

    /// <example>Analgesics</example>
    public string? CategoryName { get; set; }
}

public static class CategoryQueryExtensions
{
    public static IQueryable<Category> Filter(this IQueryable<Category> query, CategoryQueryParameter queryParameter)
    {
        if (queryParameter.CategoryCode is not null)
        {
            query = query.Where(c => c.CategoryCode!.Contains(queryParameter.CategoryCode));
        }

        if (queryParameter.CategoryName is not null)
        {
            query = query.Where(c => c.CategoryName!.Contains(queryParameter.CategoryName));
        }

        return query;
    }
}
