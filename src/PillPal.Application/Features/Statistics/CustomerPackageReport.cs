namespace PillPal.Application.Features.Statistics;

public record CustomerPackageReport
{
    public int TotalCustomerPackageSubcription { get; init; }
    public decimal TotalRevenue { get; init; }
}