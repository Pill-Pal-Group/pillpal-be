using PillPal.Application.Features.Statistics;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IStatisticService
{
    Task<CustomerRegistrationReport> GetTotalCustomerRegistrationAsync(ReportTimeRequest reportTime);
    Task<TopCustomerPackageReport?> GetTopCustomerPackageAsync(ReportTimeRequest reportTime);
    Task<CustomerPackageReport> GetCustomerPackageReportAsync(ReportTimeRequest reportTime);
    Task<IEnumerable<CustomerPackagePercentReport>> GetCustomerPackagePercentAsync(ReportTimeRequest reportTime);

    //get report by time, could be 1 month, 3 months, 6 months, 1 year
    Task<ReportByTime> GetReportsAsync(ReportTimeRequest reportTime);
}
