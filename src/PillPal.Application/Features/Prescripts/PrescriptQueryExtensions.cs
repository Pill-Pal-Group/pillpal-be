namespace PillPal.Application.Features.Prescripts;

public record PrescriptQueryParameter : PaginationQueryParameter
{
    /// <example>Dr. Doof</example>
    public string? DoctorName { get; init; }

    /// <example>General Hospital</example>
    public string? HospitalName { get; set; }
}

public class PrescriptQueryParameterValidator : AbstractValidator<PrescriptQueryParameter>
{
    public PrescriptQueryParameterValidator()
    {
        Include(new PaginationQueryParameterValidator());
    }
}

public record PrescriptIncludeParameter
{
    /// <example>true</example>
    public bool IncludePrescriptDetails { get; init; }
}

public static class PrescriptQueryExtensions
{
    public static IQueryable<Prescript> Filter(this IQueryable<Prescript> query, PrescriptQueryParameter queryParameter)
    {
        if (queryParameter.DoctorName is not null)
        {
            query = query.Where(p => p.DoctorName!.Contains(queryParameter.DoctorName));
        }

        if (queryParameter.HospitalName is not null)
        {
            query = query.Where(p => p.HospitalName!.Contains(queryParameter.HospitalName));
        }

        return query;
    }

    public static IQueryable<Prescript> Include(this IQueryable<Prescript> query, PrescriptIncludeParameter includeParameter)
    {
        if (includeParameter.IncludePrescriptDetails)
        {
            query = query.Include(p => p.PrescriptDetails);
        }

        return query;
    }
}
