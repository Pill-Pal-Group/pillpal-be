using PillPal.Application.Features.Medicines;

namespace PillPal.Application.Features.Medicines;

public record MedicineQueryParameter
{
    /// <example>MED6060-555555</example>
    public string? MedicineCode { get; init; }
    public bool? RequirePrescript { get; init; }
}

public record MedicineIncludeParameter
{
    /// <example>true</example>
    public bool IncludeSpecification { get; init; }

    /// <example>true</example>
    public bool IncludePharmaceuticalCompanies { get; init; }

    /// <example>true</example>
    public bool IncludeDosageForms { get; init; }

    /// <example>true</example>
    public bool IncludeActiveIngredients { get; init; }

    /// <example>true</example>
    public bool IncludeBrands { get; init; }
}

public static class MedicineQueryExtensions
{
    public static IQueryable<Medicine> Filter(this IQueryable<Medicine> query, MedicineQueryParameter queryParameter)
    {
        if (queryParameter.MedicineCode is not null)
        {
            query = query.Where(m => m.MedicineCode!.Contains(queryParameter.MedicineCode));
        }

        if (queryParameter.RequirePrescript is not null)
        {
            query = query.Where(m => m.RequirePrescript == queryParameter.RequirePrescript);
        }

        return query;
    }

    public static IQueryable<Medicine> Include(this IQueryable<Medicine> query, MedicineIncludeParameter includeParameter)
    {
        if (includeParameter.IncludeSpecification)
        {
            query = query.Include(m => m.Specification);
        }

        if (includeParameter.IncludePharmaceuticalCompanies)
        {
            query = query.Include(m => m.PharmaceuticalCompanies);
        }

        if (includeParameter.IncludeDosageForms)
        {
            query = query.Include(m => m.DosageForms);
        }

        if (includeParameter.IncludeActiveIngredients)
        {
            query = query.Include(m => m.ActiveIngredients);
        }

        if (includeParameter.IncludeBrands)
        {
            query = query.Include(m => m.Brands);
        }

        return query;
    }
}
