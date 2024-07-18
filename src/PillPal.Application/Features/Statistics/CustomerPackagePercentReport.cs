namespace PillPal.Application.Features.Statistics;

public record CustomerPackagePercentReport
{
    /// <example>Premium 1 month</example>
    public string? PackageName { get; init; }
    public double Percent { get; init; }
    public decimal TotalRevenue { get; set; }
}
