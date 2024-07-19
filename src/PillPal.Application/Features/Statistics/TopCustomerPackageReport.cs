namespace PillPal.Application.Features.Statistics;

public record TopCustomerPackageReport
{
    /// <example>Premium 1 month</example>
    public string? PackageName { get; init; }
    public int TotalCustomer { get; init; }
    public decimal TotalRevenue { get; init; }
}
