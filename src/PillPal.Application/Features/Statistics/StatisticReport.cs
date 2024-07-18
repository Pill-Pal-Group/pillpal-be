namespace PillPal.Application.Features.Statistics;

public record CustomerRegistration
{
    public int TotalCustomer { get; init; }
}

public record TopCustomerPackage
{
    public string? PackageName { get; init; }
    public int TotalCustomer { get; init; }
    public decimal TotalRevenue { get; init; }
}

public record CustomerPackagePercent
{
    public string? PackageName { get; init; }
    public double Percent { get; init; }
}

public record ReportByTime
{
    public IEnumerable<CustomerPackagePercent> CustomerPackagePercents { get; init; } = default!;
    public int TotalCustomerRegistration { get; init; }
    public int TotalCustomerPackageSubcription { get; init; }
    public TopCustomerPackage? TopPackage { get; init; } = default!;
    public ReportTime ReportTime { get; init; } = default!;
    public decimal Revenue { get; init; }
}

public record CustomerPackageReport
{
    public int TotalCustomerPackageSubcription { get; init; }
    public decimal TotalRevenue { get; init; }
}

public record ReportTime
{
    public DateTime? StartDate { get; set; } = default!;
    public DateTime? EndDate { get; set; } = default!;
}
