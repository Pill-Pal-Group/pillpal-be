namespace PillPal.Application.Features.Customers;

public record CustomerQueryParameter
{
    public string? CustomerCode { get; init; }
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
