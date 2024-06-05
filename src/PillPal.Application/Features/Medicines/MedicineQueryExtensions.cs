using PillPal.Application.Features.Medicines;

namespace PillPal.Application.Features.Medicines;

public record MedicineQueryParameter
{
    public string? MedicineCode { get; init; }
    public bool? RequirePrescript { get; init; }
}

public record MedicineIncludeParameter
{
    //todo: remove true init to these props
    public bool IncludeSpecification { get; init; } = true;
    public bool IncludePharmaceuticalCompanies { get; init; } = true;
    public bool IncludeDosageForms { get; init; } = true;
    public bool IncludeActiveIngredients { get; init; } = true;
    public bool IncludeBrands { get; init; } = true;
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
