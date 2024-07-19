namespace PillPal.Application.Features.Statistics;

public record ReportByTime
{
    public IEnumerable<CustomerPackagePercentReport> CustomerPackagePercents { get; init; } = default!;
    public int TotalCustomerRegistration { get; init; }
    public int TotalCustomerPackageSubcription { get; init; }
    public TopCustomerPackageReport? TopPackage { get; init; } = default!;
    public ReportTimeRequest ReportTime { get; init; } = default!;
    public decimal TotalRevenue { get; init; }
}
