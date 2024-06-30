namespace PillPal.Application.Features.PackageCategories;

public record PackageCategoryQueryParameter
{
    /// <example>False</example>
    public bool? IsDeleted { get; init; }
}

public static class PackageCategoryQueryExtensions
{
    public static IQueryable<PackageCategory> Filter(this IQueryable<PackageCategory> query, PackageCategoryQueryParameter queryParameter)
    {
        bool isDeleted = queryParameter.IsDeleted ?? false;

        query = query.Where(p => p.IsDeleted == isDeleted);
        
        return query;
    }
}