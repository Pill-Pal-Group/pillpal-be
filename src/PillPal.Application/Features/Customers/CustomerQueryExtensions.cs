namespace PillPal.Application.Features.Customers;

public record CustomerQueryParameter : PaginationQueryParameter
{
    /// <example>CUS6060-555555</example>
    public string? CustomerCode { get; init; }
}

public class CustomerQueryParameterValidator : AbstractValidator<CustomerQueryParameter>
{
    public CustomerQueryParameterValidator()
    {
        Include(new PaginationQueryParameterValidator());
    }
}

public static class CustomerQueryExtensions
{
    public static IQueryable<Customer> Filter(this IQueryable<Customer> query, CustomerQueryParameter queryParameter)
    {
        if (queryParameter.CustomerCode is not null)
        {
            query = query.Where(c => c.CustomerCode!.Contains(queryParameter.CustomerCode));
        }

        return query;
    }
}
