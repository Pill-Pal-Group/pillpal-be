using PillPal.Application.Features.Medicines;

namespace PillPal.Application.Features.Medicines;

public record MedicineQueryParameter : PaginationQueryParameter
{
    /// <example>MED6060-555555</example>
    public string? MedicineCode { get; init; }

    /// <example>VN-17384-13</example>
    public string? RegistrationNumber { get; set; }

    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid? Brand { get; init; }

    /// <example>Sedanxio</example>
    public string? MedicineName { get; init; }

    /// <example>Ethanol-based</example>
    public string? Category { get; init; }

    /// <example>Tilman S.A.</example>
    public string? PharmaceuticalCompany { get; set; }

    public bool? RequirePrescript { get; init; }
}

public class MedicineQueryParameterValidator : AbstractValidator<MedicineQueryParameter>
{
    public MedicineQueryParameterValidator()
    {
        Include(new PaginationQueryParameterValidator());
    }
}

public record MedicineIncludeParameter
{
    /// <example>true</example>
    public bool IncludeCategories { get; init; }

    /// <example>true</example>
    public bool IncludeSpecifications { get; init; }

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
        if (queryParameter.RegistrationNumber is not null)
        {
            query = query.Where(m => m.RegistrationNumber!.Contains(queryParameter.RegistrationNumber));
        }
        
        if (queryParameter.MedicineName is not null)
        {
            query = query.Where(m => m.MedicineName!.Contains(queryParameter.MedicineName));
        }

        if (queryParameter.Brand is not null)
        {
            query = query.Where(m => m.MedicineInBrands.Any(mib => mib.Brand!.Id == queryParameter.Brand));
        }

        if (queryParameter.MedicineCode is not null)
        {
            query = query.Where(m => m.MedicineCode!.Contains(queryParameter.MedicineCode));
        }

        if (queryParameter.RequirePrescript is not null)
        {
            query = query.Where(m => m.RequirePrescript == queryParameter.RequirePrescript);
        }

        if (queryParameter.Category is not null)
        {
            query = query.Where(m => m.Categories.Any(c => c.CategoryName!.Contains(queryParameter.Category)));
        }

        if (queryParameter.PharmaceuticalCompany is not null)
        {
            query = query.Where(m => m.PharmaceuticalCompanies.Any(pc => pc.CompanyName!.Contains(queryParameter.PharmaceuticalCompany)));
        }

        return query;
    }

    public static IQueryable<Medicine> Include(this IQueryable<Medicine> query, MedicineIncludeParameter includeParameter)
    {
        if (includeParameter.IncludeCategories)
        {
            query = query.Include(m => m.Categories);
        }

        if (includeParameter.IncludeSpecifications)
        {
            query = query.Include(m => m.Specification);
        }

        if (includeParameter.IncludePharmaceuticalCompanies)
        {
            query = query
                .Include(m => m.PharmaceuticalCompanies)
                .ThenInclude(m => m.Nation);
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
            query = query
                .Include(m => m.MedicineInBrands.Where(mib => !mib.IsDeleted))
                .ThenInclude(m => m.Brand);
        }

        return query;
    }
}
